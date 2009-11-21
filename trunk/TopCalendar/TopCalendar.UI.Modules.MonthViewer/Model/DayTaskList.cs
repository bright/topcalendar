using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TopCalendar.Utility.UI;

namespace TopCalendar.UI.Modules.MonthViewer.Model
{
	public class DayTaskList : NotifyPropertyChanged
	{
		private DateTime _day;
		public DateTime Day
		{
			get { return _day; }
			set { _day = value;
				OnPropertyChanged(()=>Day);
			}
		}

		private ObservableCollection<MonthTask> _taskList = new ObservableCollection<MonthTask>();
		public ObservableCollection<MonthTask> TaskList
		{
			get { return _taskList; }
			set { _taskList = value;
				OnPropertyChanged(()=>TaskList);
			}
		}

		public DayTaskList(DateTime day)
		{
			_day = day;
		}

		public void AddTask(MonthTask monthTask)
		{
			_taskList.Add(monthTask);
		}
	}
}