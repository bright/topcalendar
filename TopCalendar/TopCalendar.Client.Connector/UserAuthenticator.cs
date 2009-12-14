#region

using System;
using TopCalendar.Client.Connector.TopCalendarCommunicationService;

#endregion

namespace TopCalendar.Client.Connector
{
    public class UserAuthenticator : IUserAuthenticator
    {
        #region IUserAuthenticator Members

        private readonly ITopCalendarCommunicationService _service;
        private readonly IClientContext _clientContext;

        public UserAuthenticator(ITopCalendarCommunicationService service, IClientContext clientContext)
        {
            _service = service;
            _clientContext = clientContext;
        }

        public bool Login(string login, string password)
        {
            try
            {
                UserCredentials userCredentials = new UserCredentials {Login = login, Password = password};
                LoginUserResponse response =
                    _service.LoginUser(new LoginUserRequest {UserCredentials = userCredentials});

                _clientContext.OnUserLogged(userCredentials);

                Console.WriteLine(response);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        #endregion
    }
}