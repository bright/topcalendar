#region

using System;
using TopCalendar.Client.Connector.TopCalendarCommunicationService;

#endregion

namespace TopCalendar.Client.Connector
{
    public class UserRegistrator : IUserRegistrator
    {
        #region IUserRegistrator Members

        private readonly ITopCalendarCommunicationService _service;

        public UserRegistrator(ITopCalendarCommunicationService service)
        {
            _service = service;
        }

        public bool IsLoginFree(string login)
        {
            throw new NotImplementedException();
        }

        public void Register(string login, string password)
        {
            _service.RegisterUser(new RegisterUserRequest {Login = login, Password = password});
        }

        #endregion
    }
}