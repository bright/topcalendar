using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TopCalendar.Client.DataModel;
using TopCalendar.Utility.UI;

namespace TopCalendar.UI.Modules.WeekViewer
{
	public class HourTaskList : NotifyPropertyChanged
	{
		private DateTime _time;
		private ObservableCollection<Task> _taskList;

		public HourTaskList(DateTime time)
		{
			_time = new DateTime(time.Year,time.Month, time.Day, time.Hour, 0,0);
			_taskList = new ObservableCollection<Task>();
		}

		public DateTime Time
		{
			get { return _time; }
			private set { _time = value; 
				OnPropertyChanged(()=> Time);
			}
		}

		public ObservableCollection<Task> Tasks
		{
			get { return _taskList; }
			private set { _taskList = value;
				OnPropertyChanged(()=> Tasks);
			}
		}

		public void AddTask(Task task)
		{
			_taskList.Add(task);
		}
	}
}