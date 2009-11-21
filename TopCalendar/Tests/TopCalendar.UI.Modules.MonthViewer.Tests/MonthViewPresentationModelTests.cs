using System;
using System.Collections.ObjectModel;
using NUnit.Framework;
using Rhino.Mocks;
using TopCalendar.UI.Modules.MonthViewer.Model;
using TopCalendar.UI.Modules.MonthViewer.Services;
using TopCalendar.Utility.Tests.UI;
using TopCalendar.Utility.Tests;

namespace TopCalendar.UI.Modules.MonthViewer.Tests
{
	[TestFixture]
	public class when_getting_view_from_presentation_model
		: observations_for_presentation_model_with_stubbed_view<MonthViewPresentationModel, IMonthView> 
	{
		private IMonthView _view;
		private ObservableCollection<ObservableCollection<ObservableCollection<MonthTask>>> _returnedList;

		protected override void EstablishContext()
		{
			base.EstablishContext();
			_returnedList = new ObservableCollection<ObservableCollection<ObservableCollection<MonthTask>>>();
			Dependency<IMonthTaskLoader>()
				.Stub(loader => loader.GetTasksForMonth(DateTime.Now, 0, 0))
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
				.AssertWasCalled(loader=> loader.GetTasksForMonth(
						Arg<DateTime>.Is.Anything,
						Arg.Is(MonthViewPresentationModel.WeekCount),
						Arg.Is(MonthViewPresentationModel.DayCount)
					));
		}

		[Test]
		public void should_have_not_null_tasks()
		{
			_view.ViewModel.Tasks.ShouldNotBeNull();
		}
	}
}