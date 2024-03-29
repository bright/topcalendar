using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TopCalendar.Client.DataModel;
using TopCalendar.Utility.UI;

namespace TopCalendar.UI.Modules.MonthViewer.Model
{
	public class DayTaskList : NotifyPropertyChanged
	{
		private DateTime _day;
		public DateTime Day
		{
			get { return _day; }
			private set { _day = value;
				OnPropertyChanged(()=>Day);
			}
		}

		private ObservableCollection<Task> _taskList = new ObservableCollection<Task>();
		public ObservableCollection<Task> TaskList
		{
			get { return _taskList; }
			private set { _taskList = value;
				OnPropertyChanged(()=>TaskList);
			}
		}

		public DayTaskList(DateTime day)
		{
			Day = day;
		}

		public void AddTask(Task monthTask)
		{
			_taskList.Add(monthTask);
		}
	}
}