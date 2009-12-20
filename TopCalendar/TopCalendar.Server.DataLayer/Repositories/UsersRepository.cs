#region

using System;
using NHibernate;
using NHibernate.Criterion;
using TopCalendar.Server.DataLayer.Entities;

#endregion

namespace TopCalendar.Server.DataLayer.Repositories
{
    public class UsersRepository : BaseRepository<User, int>, IUsersRepository
    {
        public UsersRepository(ISession session) : base(session)
        {
        }


        public User GetByLogin(String login)
        {            
            return Session.CreateCriteria(typeof (User))
                .Add(Restrictions.Eq("Login", login))
                .UniqueResult<User>();
            
        }

        public User GetByLoginAndPassword(string login, string password)
        {            
            return Session.CreateCriteria(typeof(User))
                .Add(Restrictions.Eq("Login", login))
                .Add(Restrictions.Eq("Password", password))
                .UniqueResult<User>();            
        }
    }
}