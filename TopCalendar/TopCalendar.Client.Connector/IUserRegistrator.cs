using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TopCalendar.Client.Connector
{
    public interface IUserRegistrator 
    { 
        bool IsLoginFree(string login);
        void Register(string login, string password);
    }
}
