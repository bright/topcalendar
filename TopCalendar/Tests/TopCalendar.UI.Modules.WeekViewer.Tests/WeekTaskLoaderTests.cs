using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using NUnit.Framework;
using Rhino.Mocks;
using TopCalendar.Client.Connector;
using TopCalendar.Client.DataModel;
using TopCalendar.Utility.BasicExtensions;
using TopCalendar.Utility.Tests;

namespace TopCalendar.UI.Modules.WeekViewer.Tests
{
	public class when_laoding_tasks : observations_for_auto_created_sut_of_type<WeekTaskLoader>
	{
		private ObservableCollection<ObservableCollection<HourTaskList>> _result;
		private IList<Task> _listFromRepository;
		private Task _task1;
		private Task _task2;

		protected override void Because()
		{
			_result = Sut.GetTasksForWeek(DateTime.Now.Date);
		}

		protected override void EstablishContext()
		{
			_task1 = new Task("", new DateTime(2009, 12, 23,23,0,0));
			_task2 = new Task("", new DateTime(2009, 12, 21,16,59,59));
			_listFromRepository = new[]
			                      	{
			                      		_task1, 
										_task2
			                      	};
			Dependency<ITaskRepository>().Stub(repo => repo.GetTasksBetweenDates(null))
				.IgnoreArguments().Return(_listFromRepository);
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
		public void task_from_monday_should_be_at_first_element()
		{
			_result[2][23].Tasks.ShouldContain(_task1);
		}

		[Test]
		public void task_from_wednesday_should_be_at_third_element()
		{
			_result[0][16].Tasks.ShouldContain(_task2);
		}

	}
}