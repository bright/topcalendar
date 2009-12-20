#region



#endregion

namespace TopCalendar.Server.DataLayer.Entities
{
    public class User
    {

		protected User()
		{
		}

    	public User(string login, string password)
    	{
    		Login = login;
    		Password = password;
    	}

        public virtual int Id { get; private set; }
        public virtual string Login { get; set; }
        public virtual string Password { get; set; }
    }
}