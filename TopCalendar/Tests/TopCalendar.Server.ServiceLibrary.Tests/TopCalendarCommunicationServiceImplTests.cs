using System;
using NUnit.Framework;
using TopCalendar.Server.DataLayer.Entities;
using TopCalendar.Server.ServiceLibrary.ServiceContract.DataContract;
using TopCalendar.Server.ServiceLibrary.ServiceImp;
using TopCalendar.Server.ServiceLibrary.ServiceLogic;
using TopCalendar.Utility.Tests;
using Rhino.Mocks;

namespace TopCalendar.Server.ServiceLibrary.Tests
{
	public class when_service_processes_request : observations_for_auto_created_sut_of_type<TopCalendarCommunicationServiceImpl>
	{
		private RegisterUserRequest _request;
		private RegisterUserResponse _result;
		private RegisterUserResponse _expectedResponse;

		protected override void Because()
		{
			_result = Sut.RegisterUser(_request);
		}

		protected override void EstablishContext()
		{
			_request = new RegisterUserRequest
			           	{
			           		CurrentUser = new User(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()),
			           		UserCredentials = new UserCredentials {}
			           	};
			_expectedResponse = new RegisterUserResponse {};
			Dependency<IRequestToResponseLogic<RegisterUserRequest, RegisterUserResponse>>()
				.Stub(h => h.Process(null))
				.IgnoreArguments()
				.Return(_expectedResponse);
		}

		[Test]
		public void should_invoke_proper_handler()
		{
			Dependency<IRequestToResponseLogic<RegisterUserRequest,RegisterUserResponse>>()
				.AssertWasCalled(h=> h.Process(Arg.Is(_request)));
		}

		[Test]
		public void should_return_proper_result()
		{
			_expectedResponse.ShouldEqual(_result);
		}
	}
}