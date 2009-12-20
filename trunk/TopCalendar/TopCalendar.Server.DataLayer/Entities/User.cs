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

		// todo : should be moved to base class for domain objects
		public override bool Equals(object obj)
		{
			if(!typeof(User).Equals(obj.GetType()))
				return false;
			var otherUser = (User) obj;
			if(Id >0 && Id == otherUser.Id)
				return true;
			return GetHashCode().Equals(otherUser.GetHashCode());			
		}

		public override int GetHashCode()
		{
			return (typeof (User).GetHashCode() + "|" + Login + "|" + Password).GetHashCode();
		}
    }
}