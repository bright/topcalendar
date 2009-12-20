using NHibernate;
using TopCalendar.Server.DataLayer.Entities;
using TopCalendar.Server.DataLayer.Repositories;
using TopCalendar.Utility.Tests;

namespace TopCalendar.Server.DataLayer.Tests
{
    public abstract class observations_for_adding_new_user_to_repository : observations_for_auto_created_sut_of_type<UsersRepository>
    {
        protected User _user;
    	protected ISession session;


    	protected override void EstablishContext()
        {
            _user = new User( "LOGIN","PASSWORD");
            //Kernel.Bind<ISessionFactory>().ToConstant(NHibernateSessionFactory.CreateSessionFactory());
        	var sessionFactory = new InMemoryNHibernateSessionFactory().CreateSessionFactory();
			// just for now, I'll changed it tomorow ;-) Piotr
    		session = sessionFactory.OpenSession();    		
        	ProvideImplementationOf(sessionFactory);
			ProvideImplementationOf(session);

        }
        
    }
}