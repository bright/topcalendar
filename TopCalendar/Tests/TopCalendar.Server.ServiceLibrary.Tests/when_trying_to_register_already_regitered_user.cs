#region

using System;
using NUnit.Framework;
using Rhino.Mocks;
using TopCalendar.Server.DataLayer.Repositories;
using TopCalendar.Server.DataLayer.Repositories.Exceptions;
using TopCalendar.Server.ServiceLibrary.ServiceContract.DataContract;
using TopCalendar.Server.ServiceLibrary.ServiceContract.DataContract.StatusReason;
using TopCalendar.Server.ServiceLibrary.ServiceLogic;
using TopCalendar.Utility.Tests;

#endregion

namespace TopCalendar.Server.ServiceLibrary.Tests
{
    public class when_trying_to_register_already_regitered_user :
        observations_for_auto_created_sut_of_type<UserRegistrationLogic>
    {
        private RegisterUserRequest _inputData;

        private const String LoginConst = "LOGIN";
        private const String PasswordConst = "PASSWORD";

        private RegisterUserResponse _result;

        protected override void EstablishContext()
        {
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
    }
}