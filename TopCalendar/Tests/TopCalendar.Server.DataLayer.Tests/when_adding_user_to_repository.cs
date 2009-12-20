#region

using System;
using NHibernate;
using NHibernate.Criterion;
using NUnit.Framework;
using TopCalendar.Server.DataLayer.Entities;
using TopCalendar.Server.DataLayer.Repositories;
using TopCalendar.Utility.BasicExtensions;
using TopCalendar.Utility.Tests;

#endregion

namespace TopCalendar.Server.DataLayer.Tests
{
    public class when_adding_user_to_repository : observations_with_in_memory_database_of_sut_of_type<UsersRepository>
    {
    	private User _user;

    	protected override void Because()
        {
            WithinTransactionDo(s=> Sut.Add(_user));
        }

		protected override void EstablishContext()
		{
			base.EstablishContext();
			_user = new User(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
		}

    	[Test]
    	public void should_save_user_name_properly()
    	{
    		WithEntityInDatabaseDo<int,User>(_user.Id, u=> u.Login.ShouldEqual(_user.Login));
    	}

        [Test]
        public void should_save_user_passwor_properly()
        {
        	WithEntityInDatabaseDo<int,User>(_user.Id, u=> u.Password.ShouldEqual(_user.Password));
        }
    }

	public class when_retriving_user_from_repository_by_login : observations_with_in_memory_database_of_sut_of_type<UsersRepository>
	{
		private User _existingUser;
		private User _fromDb;

		protected override void Because()
		{
			_fromDb = Sut.GetByLogin(_existingUser.Login);
		}

		protected override void EstablishContext()
		{
			base.EstablishContext();
			_existingUser = new User(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
			Persist(_existingUser);
		}

		[Test]
		public void should_return_proper_user()
		{
			_fromDb.Login.ShouldEqual(_existingUser.Login);
		}
	}

	public class when_retriving_user_from_repository_by_password : observations_with_in_memory_database_of_sut_of_type<UsersRepository>
	{
		private User _user;
		private User _fromDb;

		protected override void Because()
		{
			_fromDb = Sut.GetByLoginAndPassword(_user.Login, _user.Password);
		}

		protected override void EstablishContext()
		{
			base.EstablishContext();
			_user = new User(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
			Persist(_user);
		}

		[Test]
		public void login_should_match()
		{
			_fromDb.Login.ShouldEqual(_user.Login);
		}

		[Test]
		public void password_should_match()
		{
			_fromDb.Password.ShouldEqual(_user.Password);
		}
	}

	public class when_adding_user_with_existing_login : observations_with_in_memory_database_of_sut_of_type<UsersRepository>
	{
		private User _user;
		private Exception _exception;

		protected override void Because()
		{
			
			_exception = ((Action) (
			                      	() => WithinTransactionDo(s => Sut.Add(new User(_user.Login, Guid.NewGuid().ToString())))
			                      )).ThrownException();
		}

		protected override void EstablishContext()
		{
			base.EstablishContext();
			_user = new User(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
			Persist(_user);
		}

		[Test]
		public void should_throw_exception()
		{
			_exception.ShouldNotBeNull();
		}

		[Test]
		public void should_not_add_new_user()
		{
			Session.CreateCriteria<User>().Add(Expression.Eq("Login", _user.Login)).List<User>().ShouldHaveCount(1);			                    	
		}
	}
}