using System;
using NUnit.Framework;
using TopCalendar.Client.DataModel;
using TopCalendar.Utility.Tests;

namespace TopCalendar.UI.Modules.WeekViewer.Tests
{
	public class when_creating_hour_task_list : observations_for_sut_of_type<HourTaskList>
	{
		private DateTime _time;

		protected override void Because()
		{			
		}

		protected override HourTaskList CreateSut()
		{
			return new HourTaskList(_time);
		}

		protected override void EstablishContext()
		{
			_time = DateTime.Now;
		}

		[Test]
		public void should_set_time_to_start_of_hour()
		{
			Sut.Time.ShouldEqual(new DateTime(_time.Year, _time.Month, _time.Day, _time.Hour, 0, 0));
		}
	}

	public class when_adding_task_to_hour_task_list : observations_for_sut_of_type<HourTaskList>
	{
		private Task _task;


		protected override void Because()
		{
			Sut.AddTask(_task);
		}

		protected override HourTaskList CreateSut()
		{
			return new HourTaskList(DateTime.Now);
		}

		protected override void EstablishContext()
		{
			_task = new Task();
		}

		[Test]
		public void should_contain_task()
		{
			Sut.Tasks.ShouldContain(_task);
		}
	}
}