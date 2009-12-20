using System;
using NUnit.Framework;
using TopCalendar.Server.DataLayer.Entities;
using TopCalendar.Server.DataLayer.Repositories;
using TopCalendar.Server.DataLayer.Tests;
using TopCalendar.Server.ServiceLibrary.ServiceContract.DataContract;
using TopCalendar.Server.ServiceLibrary.ServiceLogic;
using Rhino.Mocks;
using TopCalendar.Utility.Tests;

namespace TopCalendar.Server.ServiceLibrary.Tests
{
	public class when_removing_existing_task : observations_for_request_to_respone_logic<RemoveTaskLogic,RemoveTaskRequest,BaseResponse>
	{
		private int _taskId;
		private BaseResponse _result;
		private Task _task;

		protected override void Because()
		{
			_result = Sut.RemoveTask(ServiceRequest.Of<RemoveTaskRequest>(r => r.TaskId = _taskId));
		}
		protected override void EstablishContext()
		{
			base.EstablishContext();
			_taskId = new Random().Next();
			_task = New.Task().WithUser(New.User());
			Dependency<ITasksRepository>().Stub(repo => repo.GetById(0))
				.IgnoreArguments().Return(_task);
		}

		[Test]
		public void should_remove_task_with_repsitory()
		{
			Dependency<ITasksRepository>().AssertWasCalled(repo=> repo.Remove(Arg.Is(_task)));
		}

		[Test]
		public void should_return_success()
		{
			_result.Success.ShouldBeTrue();
		}
	}
}