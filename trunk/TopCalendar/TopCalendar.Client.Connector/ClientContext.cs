#region

using TopCalendar.Client.Connector.Exceptions;
using TopCalendar.Client.Connector.TopCalendarCommunicationService;

#endregion

namespace TopCalendar.Client.Connector
{
    public class ClientContext : IClientContext
    {
        private UserCredentials _userCredentials;

        public UserCredentials UserCredentials
        {
            get
            {
                if (_userCredentials == null)
                {
                    throw new UserNotLoggedException();
                }

                return _userCredentials;
            }
            private set { _userCredentials = value; }
        }

        public void OnUserLogged(UserCredentials userCredentials)
        {
            UserCredentials = userCredentials;
        }
    }
}