using NHibernate;
using Ninject.Modules;
using TopCalendar.Server.DataLayer.Repositories;

namespace TopCalendar.Server.DataLayer
{
    public class DataLayerNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ISessionFactory>().ToConstant(new NHibernateSessionFactory().CreateSessionFactory());
            Bind<IUsersRepository>().To<UsersRepository>();
            Bind<ITasksRepository>().To<TasksRepository>();
        }                                                 
    }
}
