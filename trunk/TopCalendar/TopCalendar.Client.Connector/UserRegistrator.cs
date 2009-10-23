using System;

namespace TopCalendar.Client.Connector
{
    public class UserRegistrator : IUserRegistrator
    {
        #region IUserRegistrator Members

        public bool IsLoginFree(string login)
        {
            throw new NotImplementedException();
        }

        public void Register(string login, string password)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}