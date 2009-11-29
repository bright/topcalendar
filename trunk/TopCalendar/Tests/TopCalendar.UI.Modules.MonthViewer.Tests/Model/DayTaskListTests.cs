using System;
using System.ComponentModel;
using NUnit.Framework;
using TopCalendar.Client.DataModel;
using TopCalendar.UI.Modules.MonthViewer.Model;
using TopCalendar.Utility.Tests;

namespace TopCalendar.UI.Modules.MonthViewer.Tests.Model
{
	public class when_adding_task_to_day_task_list : observations_for_sut_of_type<DayTaskList>
	{
		private Task _task;
		private DateTime _initDay;

		protected override void Because()
		{
			Sut.AddTask(_task);
		}

		protected override DayTaskList CreateSut()
		{
			return new DayTaskList(_initDay);
		}

		protected override void EstablishContext()
		{
			_task = new Task(Guid.NewGuid().ToString(), DateTime.Now);
			_initDay = DateTime.Now;
		}

		[Test]
		public void should_contain_added_task()
		{
			Sut.TaskList.ShouldContain(_task);
		}

		[Test]
		public void should_set_day_property()
		{
			Sut.Day.ShouldEqual(_initDay);
		}
	}
}