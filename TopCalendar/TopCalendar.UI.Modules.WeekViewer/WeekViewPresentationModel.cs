using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Presentation;
using Microsoft.Practices.Composite.Presentation.Commands;
using TopCalendar.Utility.UI;


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
			GoToNextWeek = new DelegateCommand<object>(GoToNextMonthImpl);
			GoToPreviousWeek = new DelegateCommand<object>(GoToPreviousWeekImpl);
			InitializeTasks();
			_view.ViewModel = this;
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

		private void GoToNextMonthImpl(object obj)
		{
			CurrentWeek = CurrentWeek.AddDays(7);
			UpdateTasksList(CurrentWeek);
		}

		public DateTime CurrentWeek
		{
			get; private set;
		}

		public ICommand GoToNextWeek
		{
			get; private set;
		}

		public ICommand GoToPreviousWeek
		{
			get;
			private set;
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