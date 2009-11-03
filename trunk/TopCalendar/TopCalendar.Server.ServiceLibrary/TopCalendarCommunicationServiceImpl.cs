using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace TopCalendar.Server.ServiceLibrary
{
    // NOTE: If you change the class name "TopCalendarCommunicationServiceImpl" here, you must also update the reference to "TopCalendarCommunicationServiceImpl" in App.config.
    public class TopCalendarCommunicationServiceImpl : ITopCalendarCommunicationService
    {
        public CheckUserResponse CheckUser(CheckUserRequest checkUserRequest)
        {
            Console.WriteLine("CheckUser, checkUserRequest: " + checkUserRequest);

            return new CheckUserResponse{Success = true};
        }

        public RegisterUserResponse RegisterUser(RegisterUserRequest registerUserRequest)
        {
            Console.WriteLine("RegisterUser, registerUserRequest: " + registerUserRequest);

            return new RegisterUserResponse { Success = true };
        }
    }
}
