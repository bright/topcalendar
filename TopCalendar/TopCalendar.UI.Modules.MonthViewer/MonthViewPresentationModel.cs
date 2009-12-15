using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Presentation.Commands;
using TopCalendar.UI.Infrastructure;
using TopCalendar.UI.Modules.MonthViewer.Model;
using TopCalendar.UI.Modules.MonthViewer.Services;
using TopCalendar.Utility.BasicExtensions;
using TopCalendar.Utility.UI;

namespace TopCalendar.UI.Modules.MonthViewer
{
	public class MonthViewPresentationModel : PresentationModelFor<IMonthView>
	{
		private readonly IMonthTaskLoader _taskLoader;
		private readonly IEventAggregator _eventAggregator;

		public static readonly int WeekCount = 5;
		public static readonly int DayCount = 7;

		private ObservableCollection<ObservableCollection<DayTaskList>> _tasks = new ObservableCollection<ObservableCollection<DayTaskList>>();
	
		public MonthViewPresentationModel(IMonthView view, IMonthTaskLoader taskLoader, IEventAggregator eventAggregator) : base(view)
		{
			_view = view;
			_taskLoader = taskLoader;
			_eventAggregator = eventAggregator;
			_view.ViewModel = this;
			GoToNextMonth = new DelegateCommand<object>(GoToNextMonthCommandImpl);
			GoToPreviousMonth = new DelegateCommand<object>(GoToPreviousMonthCommandImpl);
			ShowAddTask = new DelegateCommand<DateTime?>(ShowAddTaskImpl);
			InitializeTasks();
			SubscribeToEvents();
		}

		private void SubscribeToEvents()
		{
			_eventAggregator.GetEvent<NewTaskAddedEvent>().Subscribe((t) => InitializeTasks());
		}


		private void InitializeTasks()
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
			private set
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
			private set { _goToPreviousMonth = (DelegateCommand<object>)value; }
		}

		private void GoToPreviousMonthCommandImpl(object obj)
		{
			CurrentMonth = CurrentMonth.AddMonths(-1);
			UpdateTaskList(_taskLoader.GetTasksForMonth(CurrentMonth));
		}

		private DelegateCommand<DateTime?> _showAddTask;
		public ICommand ShowAddTask
		{
			get { return _showAddTask; }
			private set
			{
				_showAddTask = (DelegateCommand<DateTime?>)value;
				OnPropertyChanged(() => ShowAddTask);
			}
		}
		private void ShowAddTaskImpl(DateTime? obj)
		{
			_eventAggregator.GetEvent<ShowAddNewTaskViewEvent>().Publish(obj);
		}

		private void UpdateTaskList(ObservableCollection<ObservableCollection<DayTaskList>> updatedTasks)
		{
			for (int week = 0; week < WeekCount; ++week)
				for (int day = 0; day < DayCount; ++day)
					Tasks[week][day] = updatedTasks[week][day];
		}		
	}    

}