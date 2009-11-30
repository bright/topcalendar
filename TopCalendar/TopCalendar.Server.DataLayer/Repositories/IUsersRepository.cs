using System;
using TopCalendar.Server.DataLayer.Entities;

namespace TopCalendar.Server.DataLayer.Repositories
{
    public interface IUsersRepository : IRepository<User, int>
    {
        User GetByLogin(String login);

        User GetByLoginAndPassword(String login, String password);
    }
}