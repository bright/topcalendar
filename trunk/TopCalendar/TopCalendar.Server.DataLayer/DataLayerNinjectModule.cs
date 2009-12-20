using System;
using NHibernate;
using Ninject;
using Ninject.Activation;
using Ninject.Modules;
using TopCalendar.Server.DataLayer.Repositories;

namespace TopCalendar.Server.DataLayer
{
    public class DataLayerNinjectModule : NinjectModule
    {
        public override void Load()
        {
			//Todo : change session bindign
            Bind<ISessionFactory>().ToConstant(new NHibernateSessionFactory().CreateSessionFactory());
        	Bind<ISession>().ToProvider<NHiberanteSessionProvider>()
				.InThreadScope()
				.OnDeactivation(
					CloseSession()
				);
            Bind<IUsersRepository>().To<UsersRepository>();
            Bind<ITasksRepository>().To<TasksRepository>();
        }

    	private Action<ISession> CloseSession()
    	{
    		return session=> session.Dispose();
    	}
    }

	public class NHiberanteSessionProvider : IProvider
	{
		public object Create(IContext context)
		{
			var sessionFactory = context.Kernel.Get<ISessionFactory>();
			return sessionFactory.OpenSession();
		}		

		public Type Type
		{
			get { return typeof (ISession); }
		}
	}
}
