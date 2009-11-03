#region

using NHibernate;
using TopCalendar.Server.DataLayer.Entities;

#endregion

namespace TopCalendar.Server.DataLayer.Repositories
{
    public class UsersRepository : BaseRepository<User, int>, IUsersRepository
    {
        public UsersRepository(ISessionFactory sessionFactory) : base(sessionFactory)
        {
        }
    }
}