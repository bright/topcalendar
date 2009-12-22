using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Rhino.Mocks;
using TopCalendar.Utility.Tests.UI;
using TopCalendar.Utility.Tests;

namespace TopCalendar.UI.Modules.WeekViewer.Tests
{
	public class when_creating_week_view_presetnetaion_model : observations_for_presentation_model_with_stubbed_view<WeekViewPresentationModel,IWeekView>
	{
		private HourTaskList _fakeTaskList;

		protected override void Because()
		{			
		}

		protected override void EstablishContext()
		{
			base.EstablishContext();
			_fakeTaskList = new HourTaskList(DateTime.Now);
			Dependency<IWeekTaskLoader>().Stub(loader => loader.GetTasksForWeek(DateTime.Now))
				.IgnoreArguments().Return(new ObservableCollection<ObservableCollection<HourTaskList>> { new ObservableCollection<HourTaskList> { _fakeTaskList } });				
		}

		[Test]
		public void should_load_tasks_for_current_week()
		{
			Dependency<IWeekTaskLoader>().AssertWasCalled(loader => loader.GetTasksForWeek(Arg.Is(Sut.CurrentWeek)));
		}

		[Test]
		public void should_set_current_week_to_current_date()
		{
			Sut.CurrentWeek.Date.ShouldEqual(DateTime.Now.Date);
		}

		[Test]
		public void should_have_properly_intialized_tasks()
		{
			Sut.Tasks.First().ShouldContain(_fakeTaskList);
		}
	}

	public class when_going_to_next_week : observations_for_presentation_model_with_stubbed_view<WeekViewPresentationModel,IWeekView>
	{
		private ObservableCollection<ObservableCollection<HourTaskList>> _tasksList;
		private ObservableCollection<ObservableCollection<HourTaskList>> _nextTasksList;
		private HourTaskList _firstItem;
		private HourTaskList _secondItem;

		protected override void Because()
		{
			Sut.GoToNextWeek.Execute(null);
		}

		protected override void EstablishContext()
		{
			base.EstablishContext();
			_firstItem = new HourTaskList(DateTime.Now);
			_secondItem = new HourTaskList(DateTime.Now.AddDays(7));
			_tasksList = new ObservableCollection<ObservableCollection<HourTaskList>> { new ObservableCollection<HourTaskList> { _firstItem } };
			_nextTasksList = new ObservableCollection<ObservableCollection<HourTaskList>> { new ObservableCollection<HourTaskList> { _secondItem } };
	
			Dependency<IWeekTaskLoader>().Stub(loader => loader.GetTasksForWeek(DateTime.Now))
				.IgnoreArguments().Return(_tasksList).Repeat.Once();
			Dependency<IWeekTaskLoader>().Stub(loader => loader.GetTasksForWeek(DateTime.Now))
				.IgnoreArguments().Return(_nextTasksList).Repeat.Once();
		}

		[Test]
		public void should_set_current_week_to_next_week()
		{
			Sut.CurrentWeek.Date.ShouldEqual(DateTime.Now.AddDays(7).Date);
		}

		[Test]
		public void should_load_tasks_from_current_week()
		{
			Dependency<IWeekTaskLoader>().AssertWasCalled(loader=> loader.GetTasksForWeek(Arg<DateTime>.Matches(dt=> dt.Date.Equals(DateTime.Now.AddDays(7).Date))));
		}

		[Test]
		public void should_update_task_list()
		{
			Sut.Tasks.First().ShouldContain(_secondItem);
		}
	}

	public class when_going_to_previous_month : observations_for_presentation_model_with_stubbed_view<WeekViewPresentationModel,IWeekView>
	{
		private ObservableCollection<ObservableCollection<HourTaskList>> _tasksList;

		protected override void Because()
		{
			Sut.GoToPreviousWeek.Execute(null);
		}

		protected override void EstablishContext()
		{
			base.EstablishContext();
			_tasksList = new ObservableCollection<ObservableCollection<HourTaskList>> { new ObservableCollection<HourTaskList> { } };			

			Dependency<IWeekTaskLoader>().Stub(loader => loader.GetTasksForWeek(DateTime.Now))
				.IgnoreArguments().Return(_tasksList).Repeat.Twice();
		}

		[Test]
		public void should_set_current_week_to_previous_week()
		{
			Sut.CurrentWeek.Date.ShouldEqual(DateTime.Now.AddDays(-7).Date);
		}

		[Test]
		public void should_load_tasks_for_previous_week()
		{
			Dependency<IWeekTaskLoader>().AssertWasCalled(loader=> loader.GetTasksForWeek(Arg<DateTime>.Matches(dt=> dt.Date.Equals(DateTime.Now.AddDays(-7).Date))));
		}
	}
}
