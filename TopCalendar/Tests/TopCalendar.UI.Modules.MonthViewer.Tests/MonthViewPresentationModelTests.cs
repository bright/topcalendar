using System;
using System.Collections.ObjectModel;
using Microsoft.Practices.Composite.Events;
using NUnit.Framework;
using Rhino.Mocks;
using TopCalendar.UI.Infrastructure;
using TopCalendar.UI.Modules.MonthViewer.Model;
using TopCalendar.UI.Modules.MonthViewer.Services;
using TopCalendar.Utility.BasicExtensions;
using TopCalendar.Utility.Tests.UI;
using TopCalendar.Utility.Tests;
using TopCalendar.Utility.UI;

namespace TopCalendar.UI.Modules.MonthViewer.Tests
{
	
	public class when_showing_add_new_task_from_month_view_presentation_model
		: observations_for_presentation_model_with_stubbed_view<MonthViewPresentationModel,IMonthView>
	{
		private DateTime? _date;
		private ShowAddNewTaskViewEvent _addNewTaskEvent;
		private DateTime? _calledDate;

		protected override void Because()
		{
			Sut.ShowAddTask.Execute(_date);
		}

		protected override void EstablishContext()
		{
			base.EstablishContext();
			_addNewTaskEvent = new ShowAddNewTaskViewEvent();
			_addNewTaskEvent.Subscribe(dateTime =>  _calledDate = dateTime );
			Dependency<IEventAggregator>().Stub(aggr => aggr.GetEvent<ShowAddNewTaskViewEvent>()).Return(_addNewTaskEvent);
		}

		[Test]
		public void should_raise_show_addnew_task_event()
		{
			_calledDate.ShouldEqual(_date);
		}
	}

	public class when_getting_view_from_presentation_model
		: observations_for_presentation_model_with_stubbed_view<MonthViewPresentationModel, IMonthView> 
	{
		private IMonthView _view;
		private ObservableCollection<ObservableCollection<DayTaskList>> _returnedList;

		protected override void EstablishContext()
		{
			base.EstablishContext();
			_returnedList = new ObservableCollection<ObservableCollection<DayTaskList>>();
			Dependency<IMonthTaskLoader>()
				.Stub(loader => loader.GetTasksForMonth(DateTime.Now))
				.IgnoreArguments()
				.Return(_returnedList);
		}

		protected override void Because()
		{
			_view = Sut.View;
		}

		[Test]
		public void should_return_proper_view()
		{
			_view.ShouldNotBeNull();
		}

		[Test]
		public void should_set_view_model()
		{
			_view.ViewModel.ShouldEqual(Sut);
		}

		[Test]
		public void should_load_tasks_for_current_date()
		{
			Dependency<IMonthTaskLoader>()
				.AssertWasCalled(loader=> loader.GetTasksForMonth(Arg<DateTime>.Is.Anything));
		}

		[Test]
		public void should_have_not_null_tasks()
		{
			_view.ViewModel.Tasks.ShouldNotBeNull();
		}
	}

	
	public abstract class observations_for_month_view_presentation_model_changing_months
		: observations_for_presentation_model_with_stubbed_view<MonthViewPresentationModel, IMonthView>
	{
		private ObservableCollection<ObservableCollection<DayTaskList>> _fromTaskLoader;

		protected override void EstablishContext()
		{
			base.EstablishContext();
			_fromTaskLoader = new ObservableCollection<ObservableCollection<DayTaskList>>();
			// i know this is ugly but will be changed when separate class 
			//for monthview task list will be implemented
			new object[5].Each(d => _fromTaskLoader.Add(new ObservableCollection<DayTaskList>()));
			_fromTaskLoader.Each(week => new object[7].Each(o => week.Add(new DayTaskList(DateTime.Now))));
			Dependency<IMonthTaskLoader>()
				.Stub(loader => loader.GetTasksForMonth(DateTime.Now))
				.IgnoreArguments()
				.Return(_fromTaskLoader);
		}
	}

	public class when_moving_to_next_month
		: observations_for_month_view_presentation_model_changing_months
	{

		protected override void Because()
		{
			Sut.GoToNextMonth.Execute(null);
		}

		[Test]
		public void should_set_current_month_to_next_month()
		{
			Sut.CurrentMonth.IsBetween(DateTime.Now.AddMonths(1).AtMonthStart(),
				DateTime.Now.AddMonths(1).AtMonthEnd()).ShouldBeTrue();
		}

		[Test]
		public void should_get_tasks_for_next_month()
		{
			Dependency<IMonthTaskLoader>()
				.AssertWasCalled(
					loader=> loader.GetTasksForMonth(Arg.Is(Sut.CurrentMonth))
				);
		}
	}

	public class when_moving_to_previous_month
		: observations_for_month_view_presentation_model_changing_months
	{
		protected override void Because()
		{
			Sut.GoToPreviousMonth.Execute(null);
		}

		[Test]
		public void should_set_current_month_to_previous_one()
		{
			Sut.CurrentMonth.IsBetween(DateTime.Now.AddMonths(-1).AtMonthStart(),
			                           DateTime.Now.AddMonths(-1).AtMonthEnd());
		}

		[Test]
		public void should_load_task_for_previous_month_from()
		{
			Dependency<IMonthTaskLoader>()
				.AssertWasCalled(
					loader=> loader.GetTasksForMonth(Arg.Is(Sut.CurrentMonth))
				);
			
		}
	}
}