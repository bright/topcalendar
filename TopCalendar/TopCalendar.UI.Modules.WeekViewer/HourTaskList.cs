using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TopCalendar.Client.DataModel;
using TopCalendar.Utility.UI;

namespace TopCalendar.UI.Modules.WeekViewer
{
	public class HourTaskList : NotifyPropertyChanged
	{
		private readonly DateTime _time;
		private ObservableCollection<Task> _taskList = new ObservableCollection<Task>();

		public HourTaskList(DateTime time)
		{
			_time = new DateTime(time.Year,time.Month, time.Day, time.Hour, 0,0);
		}

		public DateTime Time { get { return _time; } }

		public ObservableCollection<Task> Tasks { get { return _taskList; } }

		public void AddTask(Task task)
		{
			_taskList.Add(task);
		}
	}
}