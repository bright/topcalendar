using System;
using TopCalendar.Server.DataLayer.Entities;
using TopCalendar.Utility;

namespace TopCalendar.Server.DataLayer.Tests
{

	/// <summary>
	/// creates entity objectes
	/// </summary>
	public partial class New
	{
		public static UserBuilder User()
		{
			return new UserBuilder();
		}

		public static TaskBuilder Task()
		{
			return new TaskBuilder();
		}
	}

	public class TaskBuilder : Builder<Task>
	{
		private string _description = Guid.NewGuid().ToString();
		private DateTime _startAt = DateTime.Now;
		private string _name = Guid.NewGuid().ToString();
		private DateTime? _finishAt = DateTime.Now;
		private User _user = New.User();

		public TaskBuilder()
		{			
		}

		private TaskBuilder(User user, DateTime startAt, DateTime? finishAt, string name, string description)
		{
			_user = user;
			_startAt = startAt;
			_finishAt = finishAt;
			_name = name;
			_description = description;
		}


		public override Task Build()
		{
			return new Task(_user){FinishAt = _finishAt, Name = _name, StartAt = _startAt, Description = _description};
		}

		public TaskBuilder WithUser(User user)
		{
			return NewBuilderInstance(tb => tb._user = user);
		}

		private TaskBuilder NewBuilderInstance(Action<TaskBuilder> setThings)
		{
			var ntb = new TaskBuilder(_user, _startAt, _finishAt, _name, _description);
			setThings(ntb);
			return ntb;
		}

		public TaskBuilder WithStartAt(DateTime time)
		{
			return NewBuilderInstance(tb => tb._startAt = time);
		}

		public TaskBuilder WithFinishAt(DateTime? time)
		{
			return NewBuilderInstance(tb => tb._finishAt = time);
		}
	}

	public class UserBuilder : Builder<User>
	{

		private string _password = Guid.NewGuid().ToString();
		private string _login = Guid.NewGuid().ToString();		

		public UserBuilder()
		{			
		}

		private UserBuilder(string login, string password)
		{
			_login = login;
			_password = password;			
		}

		public override User Build()
		{
			return new User(_login,_password);			
		}

		public UserBuilder WithLogin(string login)
		{
			return NewBuilderInstance(ub => ub._login = login);
		}
        
		

		private UserBuilder NewBuilderInstance(Action<UserBuilder> setThings)
		{
			var ub = new UserBuilder(_login, _password);
			setThings(ub);
			return ub;
		}

		public UserBuilder WithPassword(string password)
		{
			return NewBuilderInstance(ub => ub._password = password);
		}
	}
}