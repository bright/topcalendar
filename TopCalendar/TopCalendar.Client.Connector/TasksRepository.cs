#region

using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.Practices.Composite.Events;
using TopCalendar.Client.Connector.MappingService;
using TopCalendar.Client.Connector.TopCalendarCommunicationService;
using TopCalendar.Client.DataModel;
using TopCalendar.UI.Infrastructure;
using TopCalendar.Utility;
using TopCalendar.Utility.BasicExtensions;

#endregion

namespace TopCalendar.Client.Connector
{
	public class TasksRepository : ServiceClient, ITaskRepository
    {        
        private readonly IMappingService _mappingService;
        private readonly IEventAggregator _eventAggregator;        

        public TasksRepository(ITopCalendarCommunicationService service, IMappingService mappingService,
                               IEventAggregator eventAggregator, IClientContext clientContext)
			:base(service,clientContext)
        {            
            _mappingService = mappingService;
            _eventAggregator = eventAggregator;
        	SubscribeToEvents();
        }

		private void SubscribeToEvents()
		{
			_eventAggregator.GetEvent<DeleteTaskEvent>().Subscribe(RemoveTask);
		}

		public IList<Task> GetTasksBetweenDates(DateTimeRange dateTimeRange)
        {

            TaskSpecificationDto taskSpecificationDto = new TaskSpecificationDto
                                                            {
                                                                StartAtFrom = dateTimeRange.StartAt,
                                                                StartAtTo = dateTimeRange.FinishAt
                                                            };
        	FindTasksRequest findTasksRequest = Request<FindTasksRequest>(r => r.TaskSpecificationDto = taskSpecificationDto);

            FindTasksResponse findTasksResponse = Service.FindTasks(findTasksRequest);

            TaskDto[] tasks = findTasksResponse.Tasks;

            IList<Task> tasksList = _mappingService.FromDto(tasks);

            return tasksList;
        }

        /// <summary>
        /// Dodanie nowego zadania
        /// </summary>
        /// <param name="task">zadanie do dodania</param>
        /// <returns>true - jeœli operacja zakonczy siê pomyœlnie, jak nie to false</returns>
        public bool AddTask(Task task)
        {
            TaskDto taskDto = _mappingService.ToDto(task);
            Service.AddNewTask(Request<AddNewTaskRequest>(r=> r.Task=taskDto));
            _eventAggregator.GetEvent<NewTaskAddedEvent>().Publish(task);
			_eventAggregator.GetEvent<TaskListChangedEvent>().Publish(task.StartAt);
            return true;
            //Todo: should call TaskAddedEvent
        }

    	public void RemoveTask(Task task)
    	{
    		TaskDto taskDto = _mappingService.ToDto(task);
    		Service.RemoveTask(Request<RemoveTaskRequest>(r => r.TaskId = taskDto.Id));
			_eventAggregator.GetEvent<TaskListChangedEvent>().Publish(task.StartAt);
    	}

    	/// <summary>
        /// Update zadania
        /// </summary>
        /// <param name="task">task</param>
        /// <returns>true jeœli operacja sie powiedzie, jak nie to false</returns>
        public bool UpdateTask(Task task)
        {
    	    TaskDto taskDto = Mapper.Map(task,_mappingService.ToDto(task));
    		Service.UpdateTask(Request<UpdateTaskRequest>(r => r.Task = taskDto));
			_eventAggregator.GetEvent<TaskListChangedEvent>().Publish(task.StartAt);
            return true;
        }
    }

}