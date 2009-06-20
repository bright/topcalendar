using NHibernate;
using NHibernate.Cfg;
using ServerLib.Domain;

namespace ServerLib.Repositories
{
    /// <summary>
    /// Helper do zarz¹dzania sesj¹ Hibernate.
    /// Otwieranie nowej sesji to operacja d³ugotrwa³a i kosztowna,
    /// dlatego nowa sesja powinna byæ tworzona tylko raz (np. podczas odpalania aplikacji).
    /// </summary>
    public class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    var configuration = new Configuration();
                    configuration.Configure();
                    configuration.AddAssembly(typeof(BaseCalendarEntry).Assembly);
                    _sessionFactory = configuration.BuildSessionFactory();
                }
                return _sessionFactory;
            }
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}