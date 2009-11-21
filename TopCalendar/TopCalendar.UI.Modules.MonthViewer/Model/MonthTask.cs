using System;
using TopCalendar.Utility.UI;

namespace TopCalendar.UI.Modules.MonthViewer.Model
{
	public class MonthTask : NotifyPropertyChanged
	{
		private string _name;
		public string Name
		{
			get { return _name; }
			set { _name = value;
				OnPropertyChanged(() => Name);
			}
		}

		private DateTime _startAt;
		public DateTime StartAt
		{
			get { return _startAt; }
			set { _startAt = value;
				OnPropertyChanged(()=> StartAt);
			}
		}

		private DateTime _finishAt;
		public DateTime FinishAt
		{
			get { return _finishAt; }
			set { _finishAt = value;
				OnPropertyChanged(() => FinishAt);
			}
		}
	}
}