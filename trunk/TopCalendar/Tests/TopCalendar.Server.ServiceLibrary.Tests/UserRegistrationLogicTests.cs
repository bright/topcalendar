#region

using System;
using NUnit.Framework;
using Rhino.Mocks;
using TopCalendar.Server.DataLayer.Entities;
using TopCalendar.Server.DataLayer.Exceptions;
using TopCalendar.Server.DataLayer.Repositories;
using TopCalendar.Server.DataLayer.Tests;
using TopCalendar.Server.ServiceLibrary.ServiceContract.DataContract;
using TopCalendar.Server.ServiceLibrary.ServiceContract.DataContract.StatusReason;
using TopCalendar.Server.ServiceLibrary.ServiceLogic;
using TopCalendar.Utility.Tests;

#endregion

namespace TopCalendar.Server.ServiceLibrary.Tests
{

	public class when_registering_new_user 
		: observations_for_request_to_respone_logic<UserRegistrationLogic,RegisterUserRequest,RegisterUserResponse>
	{
		private UserCredentials _userCredentials;
		private RegisterUserResponse _result;

		protected override void Because()
		{
			_result = Sut.RegisterUser(ServiceRequest.Of<RegisterUserRequest>(r => r.UserCredentials = _userCredentials));
		}

		protected override void EstablishContext()
		{
			base.EstablishContext();
			_userCredentials = new UserCredentials {Login = Guid.NewGuid().ToString(), Password = Guid.NewGuid().ToString()};
		}

		[Test]
		public void should_add_user_to_repository()
		{
			Dependency<IUsersRepository>().AssertWasCalled(repository=> repository.Add(Arg<User>.Matches(u=> u.Login.Equals(_userCredentials.Login) && u.Password.Equals(_userCredentials.Password))));
		}

		[Test]
		public void should_return_success()
		{
			_result.Success.ShouldBeTrue();
		}
	}

	public class when_trying_to_register_already_registered_user :
        observations_for_request_to_respone_logic<UserRegistrationLogic,RegisterUserRequest,RegisterUserResponse>
    {
        private RegisterUserRequest _inputData;

        private const String LoginConst = "LOGIN";
        private const String PasswordConst = "PASSWORD";

        private RegisterUserResponse _result;

        protected override void EstablishContext()
        {
        	base.EstablishContext();
            _inputData = new RegisterUserRequest
                             {
                                 UserCredentials = new UserCredentials
                                                       {
                                                           Login = LoginConst,
                                                           Password = PasswordConst
                                                      }
                             };
            _result = null;        	
            Dependency<IUsersRepository>()
                .Stub(x => x.Add(null))
                .IgnoreArguments()
                .Throw(new UserLoginAlreadyTakenException());
        }

        protected override void Because()
        {
            _result = Sut.RegisterUser(_inputData);
        }

        [Test]
        public void should_return_response_with_status_reason_login_already_taken()
        {
            _result.StatusReason.ShouldEqual(StatusReasonFor.RegisterUser.LOGIN_ALREADY_TAKEN);
        }

        [Test]
        public void should_return_response_with_success_set_to_false()
        {
            _result.Success.ShouldEqual(false);
        }

    	[Test]
    	public void should_call_add_user_to_repository()
    	{
    		Dependency<IUsersRepository>().AssertWasCalled(repository=> repository.Add(Arg<User>.Matches(u=> u.Login.Equals(_inputData.UserCredentials.Login))));
    	}
    }
}