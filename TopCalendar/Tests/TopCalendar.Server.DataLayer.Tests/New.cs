using System;
using TopCalendar.Server.DataLayer.Entities;
using TopCalendar.Utility;

namespace TopCalendar.Server.DataLayer.Tests
{

	/// <summary>
	/// creates entity objectes
	/// </summary>
	public class New
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
		private User _user;

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

		public override User Build()
		{
			return new User(_login,_password);
		}
	}
}