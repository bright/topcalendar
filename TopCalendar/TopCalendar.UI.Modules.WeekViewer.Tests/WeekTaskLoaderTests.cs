using System;
using System.Collections.ObjectModel;
using NUnit.Framework;

using TopCalendar.Utility.BasicExtensions;
using TopCalendar.Utility.Tests;

namespace TopCalendar.UI.Modules.WeekViewer.Tests
{
	public class when_laoding_tasks : observations_for_auto_created_sut_of_type<WeekTaskLoader>
	{
		private ObservableCollection<ObservableCollection<HourTaskList>> _result;

		protected override void Because()
		{
			_result = Sut.GetTasksForWeek(DateTime.Now.Date);
		}

		[Test]
		public void should_return_list_containg_seven_elements()
		{
			_result.Count.ShouldEqual(7);
		}

		[Test]
		public void each_element_of_returned_list_should_contain_24_elements()
		{
			_result.Each(sub => sub.Count.ShouldEqual(24));
		}

		[Test]
		public void each_element_of_returend_list_should_represent_week_of_month()
		{			
		}
	}
}