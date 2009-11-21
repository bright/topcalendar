using System;
using System.Collections.Generic;
using TopCalendar.Client.Connector.Model;
using TopCalendar.Client.Connector.TopCalendarCommunicationService;
using TopCalendar.Utility;
using TopCalendar.Utility.BasicExtensions;

namespace TopCalendar.Client.Connector
{
	public class TasksRepository : ITaskRepository
	{
		private readonly ITopCalendarCommunicationService _service;

		public TasksRepository(ITopCalendarCommunicationService service)
		{
			_service = service;
		}


		public IList<Task> GetTasksBetweenDates(DateTime start, DateTime stop)
		{
			Check.Guard(stop.CompareTo(start)>0,  "Data {0} powinna byc wczeœniejsza od {1}".ToFormat(start, stop));
			//Todo: Michal
			return Fake.TaskList();
		}
	}

	public interface ITaskRepository
	{
		IList<Task> GetTasksBetweenDates(DateTime start, DateTime stop);
	}

	public static class Fake
	{
		public static IList<Task> TaskList()
		{
			return new List<Task>
			       	{
			       		new Task {Name = "Pierwsze", StartAt = DateTime.Now, FinishAt = DateTime.Now.AddDays(1)},
			       		new Task {Name = "Drugie", StartAt = DateTime.Now, FinishAt = DateTime.Now.AddHours(1)}
			       	};
		}
	}
}