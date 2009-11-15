#region

using System;
using TopCalendar.Server.ServiceLibrary.ServiceContract;
using TopCalendar.Server.ServiceLibrary.ServiceContract.DataContract;
using TopCalendar.Server.ServiceLibrary.ServiceLogic;

#endregion

namespace TopCalendar.Server.ServiceLibrary.ServiceImp
{
    public class TopCalendarCommunicationServiceImpl : ITopCalendarCommunicationService
    {
        private readonly UserRegistrationLogic _userRegistrationLogic;

        public TopCalendarCommunicationServiceImpl(UserRegistrationLogic userRegistrationLogic)
        {
            _userRegistrationLogic = userRegistrationLogic;
        }

        public CheckUserResponse CheckUser(CheckUserRequest checkUserRequest)
        {
            Console.WriteLine("CheckUser, checkUserRequest: " + checkUserRequest);

            return new CheckUserResponse {Success = true};
        }

        public RegisterUserResponse RegisterUser(RegisterUserRequest registerUserRequest)
        {
            Console.WriteLine("RegisterUser, registerUserRequest: " + registerUserRequest);

            RegisterUserResponse registerUserResponse =
                _userRegistrationLogic.RegisterUser(registerUserRequest);

            return registerUserResponse;
        }
    }
}