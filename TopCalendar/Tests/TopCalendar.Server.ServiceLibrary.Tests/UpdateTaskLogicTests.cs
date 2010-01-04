using System;
using NUnit.Framework;
using Rhino.Mocks;
using TopCalendar.Server.DataLayer.Entities;
using TopCalendar.Server.DataLayer.Repositories;
using TopCalendar.Server.DataLayer.Tests;
using TopCalendar.Server.ServiceLibrary.ServiceContract.DataContract;
using TopCalendar.Server.ServiceLibrary.ServiceContract.DataContract.Dto;
using TopCalendar.Server.ServiceLibrary.ServiceLogic;
using TopCalendar.Utility.Tests;

namespace TopCalendar.Server.ServiceLibrary.Tests
{
	public class when_updating_task : observations_for_request_to_respone_logic<UpdateTaskLogic,UpdateTaskRequest,BaseResponse>
	{
		private TaskDto _taskDto;
		private BaseResponse _result;
		private Task _task;		
		private UpdateTaskRequest _request;

		protected override void Because()
		{
			_result = Sut.Process(_request);
		}

		protected override void EstablishContext()
		{
			base.EstablishContext();
			_taskDto = new TaskDto { Description = Guid.NewGuid().ToString(), FinishAt = DateTime.Now, StartAt = DateTime.Now, Name = Guid.NewGuid().ToString() };			
			_request = ServiceRequest.Of<UpdateTaskRequest>(r => r.Task = _taskDto);			
			_task = New.Task().WithUser(_request.CurrentUser);
			Dependency<ITasksRepository>().Stub(rep => rep.GetById(0)).IgnoreArguments().Return(_task);
		}

		[Test]
		public void should_update_task_name()
		{
			_task.Name.ShouldEqual(_taskDto.Name);
		}

		[Test]
		public void should_update_task_description()
		{
			_task.Description.ShouldEqual(_taskDto.Description);
		}

		[Test]
		public void should_update_task_startAt()
		{
			_task.StartAt.ShouldEqual(_taskDto.StartAt);
		}

		[Test]
		public void should_update_task_fisnih()
		{
			_task.FinishAt.ShouldEqual(_taskDto.FinishAt);
		}

		[Test]
		public void should_return_success()
		{
			_result.Success.ShouldBeTrue();
		}
	}

	public class when_udpating_task_with_malformed_user : observations_for_request_to_respone_logic<UpdateTaskLogic,UpdateTaskRequest,BaseResponse>
	{
		private UpdateTaskRequest _request;
		private BaseResponse _result;
		private TaskDto _taskDto;

		protected override void Because()
		{
			_result = Sut.Process(_request);
		}

		protected override void EstablishContext()
		{
			base.EstablishContext();
			_taskDto = new TaskDto();
			_request = ServiceRequest.Of<UpdateTaskRequest>(r => r.Task = _taskDto);
			Dependency<ITasksRepository>().Stub(rep => rep.GetById(0)).IgnoreArguments().Return(New.Task());
		}

		[Test]
		public void should_return_error()
		{
			_result.Success.ShouldBeFalse();
		}
	}
}