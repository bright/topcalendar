#region

using System;
using TopCalendar.Client.Connector.TopCalendarCommunicationService;

#endregion

namespace TopCalendar.Client.Connector
{
    public class UserRegistrator : ServiceClient, IUserRegistrator
    {
        #region IUserRegistrator Members
        
        public UserRegistrator(ITopCalendarCommunicationService service, IClientContext clientContext)
			:base(service,clientContext)
        {
        }

        public bool IsLoginFree(string login)
        {
            throw new NotImplementedException();
        }

        public bool Register(string login, string password)
        {
            try
            {
                UserCredentials userCredentials = new UserCredentials {Login = login, Password = password};
                RegisterUserResponse response =
                    Service.RegisterUser(new RegisterUserRequest {UserCredentials = userCredentials});

                if (response.Success)
                {
                    ClientContext.OnUserLogged(userCredentials);
                }

                Console.WriteLine(response);

                return response.Success;
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