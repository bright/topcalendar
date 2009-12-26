using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Presentation;
using Microsoft.Practices.Composite.Presentation.Commands;
using TopCalendar.UI.Infrastructure;
using TopCalendar.Utility.UI;
using TopCalendar.Utility.BasicExtensions;


namespace TopCalendar.UI.Modules.WeekViewer
{
	public class WeekViewPresentationModel : PresentationModelFor<IWeekView>
	{
		private readonly IWeekTaskLoader _taskLoader;
		private readonly IEventAggregator _eventAggregator;
		

		public WeekViewPresentationModel(IWeekView view, IWeekTaskLoader taskLoader, IEventAggregator eventAggregator): base(view)
		{
			_taskLoader = taskLoader;
			_taskLoader = taskLoader;
			CurrentWeek = DateTime.Now;
			_eventAggregator = eventAggregator;
			GoToNextWeek = new DelegateCommand<object>(GoToNextWeekImpl);
			GoToPreviousWeek = new DelegateCommand<object>(GoToPreviousWeekImpl);
			InitializeTasks();
			SubscribeToEvents();
			_view.ViewModel = this;
		}

		private void SubscribeToEvents()
		{
			_eventAggregator.GetEvent<TaskListChangedEvent>().Subscribe(HandleTaskListChaged);
		}

		private void HandleTaskListChaged(DateTime changedTaskStartAt)
		{
			if(changedTaskStartAt.IsBetween(CurrentWeek.AtWeekStart(),CurrentWeek.AtWeekEnd()))
			{
				UpdateTasksList(CurrentWeek);
			}
		}

		private void InitializeTasks()
		{
			Tasks = _taskLoader.GetTasksForWeek(CurrentWeek);
		}

		private void GoToPreviousWeekImpl(object obj)
		{
			CurrentWeek = CurrentWeek.AddDays(-7);
			UpdateTasksList(CurrentWeek);
		}

		private void UpdateTasksList(DateTime time)
		{
			var tasks = _taskLoader.GetTasksForWeek(CurrentWeek);
			for(int i = 0; i < Tasks.Count; ++i)
				for(int j = 0; j <Tasks[i].Count; ++j)
				{
					Tasks[i][j] = tasks[i][j];
				}
		}

		private void GoToNextWeekImpl(object obj)
		{
			CurrentWeek = CurrentWeek.AddDays(7);
			UpdateTasksList(CurrentWeek);
		}

		private DateTime _currentWeek;

		public DateTime CurrentWeek
		{
			get { return _currentWeek; }
			private set { _currentWeek = value; 
				OnPropertyChanged(()=> CurrentWeek);
			}
		}

		private ICommand _goToNextWeek;

		public ICommand GoToNextWeek
		{
			get { return _goToNextWeek; }
			private set { _goToNextWeek = value; 
				OnPropertyChanged(()=> GoToNextWeek);
			}
		}

		private ICommand _goToPreviousWeek;

		public ICommand GoToPreviousWeek
		{
			get { return _goToPreviousWeek; }
			private set { _goToPreviousWeek = value; 
				OnPropertyChanged(()=> GoToPreviousWeek);
			}
		}

		public ObservableCollection<ObservableCollection<HourTaskList>> Tasks { get; private set; }
	}

	public interface IWeekTaskLoader
	{
		ObservableCollection<ObservableCollection<HourTaskList>> GetTasksForWeek(DateTime time);
	}

	public interface IWeekView : IViewForModel<IWeekView, WeekViewPresentationModel>
	{
	}


}