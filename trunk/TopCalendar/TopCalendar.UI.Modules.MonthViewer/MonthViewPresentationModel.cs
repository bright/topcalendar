using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Practices.Composite.Presentation.Commands;
using TopCalendar.UI.Modules.MonthViewer.Model;
using TopCalendar.UI.Modules.MonthViewer.Services;
using TopCalendar.Utility.BasicExtensions;
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
			_goToNextMonth = new DelegateCommand<object>(GoToNextMonthCommandImpl);
			_goToPreviousMonth = new DelegateCommand<object>(GoToPreviousMonthCommandImpl);
			Initialize();
		}		

		private void Initialize()
		{
			Tasks = _taskLoader.GetTasksForMonth(CurrentMonth);
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

		private DateTime _currentMonth = DateTime.Now;
		public DateTime CurrentMonth
		{
			get { return _currentMonth; }
			set { _currentMonth = value; 
				OnPropertyChanged(()=> CurrentMonth);
			}
		}

		private DelegateCommand<object> _goToNextMonth;
		public ICommand GoToNextMonth
		{
			get { return _goToNextMonth; }
			set
			{
				_goToNextMonth = (DelegateCommand<object>)value;
				OnPropertyChanged(() => GoToNextMonth);
			}
		}
		private void GoToNextMonthCommandImpl(object obj)
		{
			CurrentMonth = CurrentMonth.AddMonths(1);
			UpdateTaskList(_taskLoader.GetTasksForMonth(CurrentMonth));
		}

		private DelegateCommand<object> _goToPreviousMonth;
		public ICommand GoToPreviousMonth
		{
			get { return _goToPreviousMonth; }
			set { _goToPreviousMonth = (DelegateCommand<object>)value; }
		}

		private void GoToPreviousMonthCommandImpl(object obj)
		{
			CurrentMonth = CurrentMonth.AddMonths(-1);
			UpdateTaskList(_taskLoader.GetTasksForMonth(CurrentMonth));
		}


		private void UpdateTaskList(ObservableCollection<ObservableCollection<DayTaskList>> updatedTasks)
		{
			for (int week = 0; week < WeekCount; ++week)
				for (int day = 0; day < DayCount; ++day)
					Tasks[week][day] = updatedTasks[week][day];
		}		
	}    

}