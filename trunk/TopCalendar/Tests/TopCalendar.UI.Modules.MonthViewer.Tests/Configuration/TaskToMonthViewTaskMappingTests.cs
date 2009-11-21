using System;
using AutoMapper;
using NUnit.Framework;
using TopCalendar.Client.Connector.Model;
using TopCalendar.UI.Modules.MonthViewer.Configuration;
using TopCalendar.UI.Modules.MonthViewer.Model;
using TopCalendar.Utility.Tests;
using TopCalendar.Utility.BasicExtensions;

namespace TopCalendar.UI.Modules.MonthViewer.Tests.Configuration
{
	[TestFixture]
	public class when_mapping_task_to_month_task
		: observations_for_mapping_configuration_defined_in<TaskToMonthViewTaskMapping>
	{
		private Task _task;
		private Exception _exception;
		private MonthTask _result;

		protected override void EstablishContext()
		{
			base.EstablishContext();
			Task task = new Task {Name = "Name", StartAt = new DateTime(1999, 10, 12), FinishAt=new DateTime(1999,12,10)};

		}

		protected override void Because()
		{
			_exception = ((Action) (() => _result = Mapper.Map<Task, MonthTask>(_task))).ThrownException();
		}

		[Test]
		public void mapper_configuration_should_be_valid()
		{
			assert_mapper_configuration_should_be_valid();
		}

		[Test]
		public void should_throw_no_exceptions()
		{
			_exception.ShouldBeNull();
		}

	}
}