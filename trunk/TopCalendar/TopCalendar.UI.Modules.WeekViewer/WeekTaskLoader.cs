using System;
using System.Collections.ObjectModel;
using TopCalendar.Client.Connector;

using TopCalendar.Utility.UI;

namespace TopCalendar.UI.Modules.WeekViewer
{
	public class WeekTaskLoader : IWeekTaskLoader
	{
		private readonly ITaskRepository _taskRepository;
		private int DaysInWeek = 7;
		private int HoursInDay = 24;

		public WeekTaskLoader(ITaskRepository taskRepository)
		{
			_taskRepository = taskRepository;
		}

		public ObservableCollection<ObservableCollection<HourTaskList>> GetTasksForWeek(DateTime time)
		{
			var result = GetResultList();
			return result;
		}

		private ObservableCollection<ObservableCollection<HourTaskList>> GetResultList()
		{
			var result = new ObservableCollection<ObservableCollection<HourTaskList>>();
			for (int i = 0; i < DaysInWeek; ++i)
			{
				var hourTaskLists = new ObservableCollection<HourTaskList>();
				result.Add(hourTaskLists);
				for (var h = 0; h < HoursInDay; ++h)
					hourTaskLists.Add(new HourTaskList(DateTime.Now));
			}
			return result;
		}
	}

	public class HourTaskList : NotifyPropertyChanged
	{
		private readonly DateTime _time;

		public HourTaskList(DateTime time)
		{
			_time = new DateTime(time.Year,time.Month, time.Day, time.Hour, 0,0);
		}

		public DateTime Time { get { return _time; } }
	}
}