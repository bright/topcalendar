#region

using System;
using NHibernate;
using NUnit.Framework;
using TopCalendar.Server.DataLayer.Entities;
using TopCalendar.Utility.Tests;

#endregion

namespace TopCalendar.Server.DataLayer.Tests
{
    public class when_adding_new_user_to_repository : observations_for_adding_new_user_to_repository
    {

    	public void WithinTransactionDo(Action doJob)
		{
			using(var t = session.BeginTransaction())
			{
				doJob();
				t.Commit();
			}
		}

    	protected override void Because()
        {
            WithinTransactionDo(()=>Sut.Add(_user));
            // when user object is saved properly to db
            // nhibernate sets it's id field
        }

        [Test]
        public void should_be_able_to_fetch_saved_user_by_login()
        {
			WithinTransactionDo(()=>
        	{
				User fetchedFromDb = Sut.GetByLogin(_user.Login);

				ShouldEqualToSavedUser(fetchedFromDb);
        	});
            
        }

        [Test]
        public void should_be_able_to_fetch_saved_user_by_id()
        {
        	WithinTransactionDo(() =>
        	                    	{
        	                    		User fetchedFromDb = Sut.GetById(_user.Id);

        	                    		ShouldEqualToSavedUser(fetchedFromDb);
        	                    	});
        }

        private void ShouldEqualToSavedUser(User fetchedFromDb)
        {
            fetchedFromDb.Id.ShouldEqual(_user.Id);
            fetchedFromDb.Login.ShouldEqual(_user.Login);
            fetchedFromDb.Password.ShouldEqual(_user.Password);
        }
    }
}