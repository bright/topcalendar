using System;
using NHibernate;
using Ninject.Activation;

namespace TopCalendar.Server.Bootstrap
{
	public class NHSessionProvider : IProvider
	{
		private readonly ISessionFactory _sessionFactory;

		public NHSessionProvider(ISessionFactory sessionFactory)
		{
			_sessionFactory = sessionFactory;
		}

		public object Create(IContext context)
		{
			return _sessionFactory.OpenSession();
		}

		public Type Type
		{
			get { return typeof (ISession); }
		}
	}
}