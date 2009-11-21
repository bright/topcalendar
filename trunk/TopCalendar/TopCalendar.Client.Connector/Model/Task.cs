using System;

namespace TopCalendar.Client.Connector.Model
{
	public class Task
	{
		public string Name { get; set; }

		public DateTime StartAt { get; set; }

		public DateTime FinishAt { get; set; }

		public string Description { get; set; }
	}
}