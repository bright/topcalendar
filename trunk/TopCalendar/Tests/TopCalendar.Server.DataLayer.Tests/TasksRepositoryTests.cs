using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TopCalendar.Server.DataLayer.Entities;
using TopCalendar.Server.DataLayer.Repositories;
using TopCalendar.Server.DataLayer.Specifications;
using TopCalendar.Utility.BasicExtensions;
using TopCalendar.Utility.Tests;

namespace TopCalendar.Server.DataLayer.Tests
{
	public class when_finding_task_using_task_specificatation : observations_with_in_memory_database_of_sut_of_type<TasksRepository>
	{
		private TaskSpecification _taskSpecficitation;
		private IList<Task> _result;
		private User _user;
		private DateTime _dateStart;
		private DateTime _dateStop;
		private Task _task1;
		private Task _task2;

		protected override void Because()
		{
			_result = Sut.Find(_taskSpecficitation);
		}

		protected override void EstablishContext()
		{
			base.EstablishContext();
			_user = New.User();
			Persist(_user);
			_dateStart = DateTime.Now.AddDays(-12);
			_dateStop = _dateStart.AddDays(13);
			_task1 = New.Task().WithUser(_user).WithStartAt(_dateStart.AddDays(1)).WithFinishAt(_dateStop.AddHours(-1));
			Persist(_task1);
			_task2 = New.Task().WithUser(_user).WithStartAt(_dateStart.AddDays(-1)).WithFinishAt(_dateStop.AddHours(-1));
			Persist(_task2);
			_taskSpecficitation = new TaskSpecification(_user){StartAtFrom = _dateStart, StartAtTo = _dateStop};
		}

		[Test]
		public void should_find_matching_tasks()
		{
			_result.ShouldContain(t => t.Id.Equals(_task1.Id));
		}

		[Test]
		public void should_not_return_not_matching_tasks()
		{
			_result.ShouldNotContain(t=> t.Id.Equals(_task2.Id));
		}
	}


	public class when_adding_task_without_user_to_repository : observations_with_in_memory_database_of_sut_of_type<TasksRepository>
	{
		
		private Task _task;
		private Exception _exception;

		protected override void Because()
		{
			_exception = ((Action)(()=>WithinTransactionDo(s=> Sut.Add(_task)))).ThrownException();
		}

		protected override void EstablishContext()
		{
			base.EstablishContext();
			_task = new Task(New.User());
			_task.User = null;
		}

		[Test]
		public void should_throw_exception()
		{
			_exception.ShouldNotBeNull();
		}
	}
}