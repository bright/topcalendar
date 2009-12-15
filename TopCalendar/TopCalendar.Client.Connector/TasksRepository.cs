#region

using System;
using System.Collections.Generic;
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
    public class TasksRepository : ITaskRepository
    {
        private readonly ITopCalendarCommunicationService _service;
        private readonly IMappingService _mappingService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IClientContext _clientContext;

        public TasksRepository(ITopCalendarCommunicationService service, IMappingService mappingService,
                               IEventAggregator eventAggregator, IClientContext clientContext)
        {
            _service = service;
            _mappingService = mappingService;
            _eventAggregator = eventAggregator;
            _clientContext = clientContext;
        }

        public IList<Task> GetTasksBetweenDates(DateTimeRange dateTimeRange)
        {

            TaskSpecificationDto taskSpecificationDto = new TaskSpecificationDto
                                                            {
                                                                StartAtFrom = dateTimeRange.StartAt,
                                                                StartAtTo = dateTimeRange.FinishAt
                                                            };
            FindTasksRequest findTasksRequest = new FindTasksRequest
                                                    {
                                                        TaskSpecificationDto = taskSpecificationDto,
                                                        UserCredentials = _clientContext.UserCredentials
                                                    };

            FindTasksResponse findTasksResponse = _service.FindTasks(findTasksRequest);

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
            _service.AddNewTask(new AddNewTaskRequest {Task = taskDto, UserCredentials = _clientContext.UserCredentials});
            _eventAggregator.GetEvent<NewTaskAddedEvent>().Publish(task);
            return true;
            //Todo: should call TaskAddedEvent
        }

    	public void RemoveTask(Task task)
    	{
    		//Todo: Michale jak zupdatowac referencje do serwisu	
    	}

    	/// <summary>
        /// Update zadania
        /// </summary>
        /// <param name="task">task</param>
        /// <returns>true jeœli operacja sie powiedzie, jak nie to false</returns>
        public bool UpdateTask(Task task)
        {
            // TODO 
            return true;
        }
    }

}