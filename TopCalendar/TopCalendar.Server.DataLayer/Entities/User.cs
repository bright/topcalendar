#region



#endregion

using TopCalendar.Utility;
using TopCalendar.Utility.BasicExtensions;

namespace TopCalendar.Server.DataLayer.Entities
{
    public class User
    {
		protected User()
		{
		}

    	public User(string login, string password)
    	{
    		Check.Guard(!login.IsEmpty(), "User login should not be empty");
    		Check.Guard(!password.IsEmpty(), "User password should not be empty");
    		Login = login;
    		Password = password;
    	}

        public virtual int Id { get; private set; }
        public virtual string Login { get; set; }
        public virtual string Password { get; set; }
    }
}