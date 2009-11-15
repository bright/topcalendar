#region

using System;
using NHibernate;
using NHibernate.Criterion;
using TopCalendar.Server.DataLayer.Entities;
using TopCalendar.Server.DataLayer.Repositories.Exceptions;

#endregion

namespace TopCalendar.Server.DataLayer.Repositories
{
    public class UsersRepository : BaseRepository<User, int>, IUsersRepository
    {
        public UsersRepository(ISessionFactory sessionFactory) : base(sessionFactory)
        {
        }

        public override User Add(User entity)
        {
            using (ISession session = GetSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                // jak znajdzie sie lepszy sposob na sprawdzenia unikalnosci
                // to z przyjemnoscia usune to GetByLogin :)
                // (wyjatek rzucany przez NHibernate gdy baza odmowi dodania rekordu 
                // ze wzgledu na naruszenie constraint jest niewystarczajacy)
                if (GetByLogin(entity.Login) != null)
                {
                    transaction.Rollback();
                    throw new UserLoginAlreadyTakenException();
                }

                session.Save(entity);

                transaction.Commit();

                return entity;
            }
        }

        public User GetByLogin(String login)
        {
            using (var session = GetSession())
            {
                return session.CreateCriteria(typeof (User))
                    .Add(Expression.Eq("Login", login))
                    .UniqueResult<User>();
            }
        }
    }
}