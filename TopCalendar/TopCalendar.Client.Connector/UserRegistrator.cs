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
        private readonly IClientContext _clientContext;

        public UserRegistrator(ITopCalendarCommunicationService service, IClientContext clientContext)
        {
            _service = service;
            _clientContext = clientContext;
        }

        public bool IsLoginFree(string login)
        {
            throw new NotImplementedException();
        }

        public void Register(string login, string password)
        {
            try
            {
                UserCredentials userCredentials = new UserCredentials{Login = login, Password = password};
                RegisterUserResponse response =
                    _service.RegisterUser(new RegisterUserRequest{UserCredentials = userCredentials} );

                _clientContext.OnUserLogged(userCredentials);

                Console.WriteLine(response);
            } catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
      
        }

        #endregion
    }
}