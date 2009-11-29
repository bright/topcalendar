using System;
using System.Collections.Generic;
using Microsoft.Practices.Composite.Events;
using TopCalendar.Client.Connector.TopCalendarCommunicationService;
using TopCalendar.Client.DataModel;
using TopCalendar.UI.Infrastructure;
using TopCalendar.Utility;
using TopCalendar.Utility.BasicExtensions;

namespace TopCalendar.Client.Connector
{
	public class TasksRepository : ITaskRepository
	{
		private readonly ITopCalendarCommunicationService _service;
	    private readonly IEventAggregator _eventAggregator;

	    public TasksRepository(ITopCalendarCommunicationService service, IEventAggregator eventAggregator)
		{
		    _service = service;
		    _eventAggregator = eventAggregator;
		}


	    public IList<Task> GetTasksBetweenDates(DateTimeRange dateTimeRange)
		{
			//Check.Guard(stop.CompareTo(start)>0,  "Data {0} powinna byc wczeœniejsza od {1}".ToFormat(start, stop));
			//Todo: Michal
			return Fake.TaskList();
		}

        /// <summary>
        /// Dodanie nowego zadania
        /// </summary>
        /// <param name="task">zadanie do dodania</param>
        /// <returns>true - jeœli operacja zakonczy siê pomyœlnie, jak nie to false</returns>
        public bool AddTask(Task task)
        {
            Fake.TaskList().Add(task);
            _eventAggregator.GetEvent<NewTaskAddedEvent>().Publish(task);
            return true;
            //Todo: should call TaskAddedEvent
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

	public interface ITaskRepository
	{
		IList<Task> GetTasksBetweenDates(DateTimeRange dateTimeRange);
        bool UpdateTask(Task task);
        bool AddTask(Task task);
	}

	public static class Fake
	{
        static IList<Task> _taskList = new List<Task>
			       	{
			       		new Task("Pierwsze",DateTime.Now){ FinishAt = DateTime.Now.AddDays(1)},
			       		new Task("Drugie",DateTime.Now){ FinishAt = DateTime.Now.AddHours(1)}
			       	};

		public static IList<Task> TaskList()
		{
		    return _taskList;
		}
	}
}