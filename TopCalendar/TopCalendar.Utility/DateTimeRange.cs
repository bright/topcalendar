using System;
using TopCalendar.Utility.BasicExtensions;

namespace TopCalendar.Utility
{
	public class DateTimeRange
	{
		private readonly DateTime _start;
		private readonly DateTime _stop;

		public DateTimeRange(DateTime start, DateTime stop)
		{
			Check.Guard(stop.CompareTo(start) > 0, "{0} powinna byæ póŸniejsza ni¿ {1}".ToFormat(start, stop));
			_start = start;
			_stop = stop;
		}

		public DateTime StartAt { get { return _start; } }
		public DateTime FinishAt { get { return _stop; } }

		public bool IsBetween(DateTime dateTime)
		{
			return dateTime.IsBetween(StartAt, FinishAt);
		}
	}
}