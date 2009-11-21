using System;
using System.Collections.ObjectModel;
using TopCalendar.UI.Modules.MonthViewer.Model;
using TopCalendar.UI.Modules.MonthViewer.Services;
using TopCalendar.Utility.UI;

namespace TopCalendar.UI.Modules.MonthViewer
{
	public class MonthViewPresentationModel : PresentationModelFor<IMonthView>
	{
		private readonly IMonthView _view;
		private readonly IMonthTaskLoader _taskLoader;

		public static readonly int WeekCount = 5;
		public static readonly int DayCount = 7;

		private ObservableCollection<ObservableCollection<DayTaskList>> _tasks = new ObservableCollection<ObservableCollection<DayTaskList>>();
	
		public MonthViewPresentationModel(IMonthView view, IMonthTaskLoader taskLoader) : base(view)
		{
			_view = view;
			_taskLoader = taskLoader;
			_view.ViewModel = this;
			Initialize();
		}

		private void Initialize()
		{
			Tasks = _taskLoader.GetTasksForMonth(DateTime.Now, WeekCount, DayCount);
		}

		public ObservableCollection<ObservableCollection<DayTaskList>> Tasks
		{
			get{ return _tasks;}
			private set
			{
				_tasks = value;
				OnPropertyChanged(()=> Tasks);
			}
		}
	}    

}