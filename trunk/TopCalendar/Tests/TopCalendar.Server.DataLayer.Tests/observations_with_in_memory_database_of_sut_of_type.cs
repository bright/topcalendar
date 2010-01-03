using System;
using NHibernate;
using TopCalendar.Utility.Tests;

namespace TopCalendar.Server.DataLayer.Tests
{
	public abstract class observations_with_in_memory_database_of_sut_of_type<TSut> : observations_for_auto_created_sut_of_type<TSut>
		where TSut : class 
	{
		private static readonly InMemoryNHibernateSessionFactory _inMemoryNHibernateSessionFactory = new InMemoryNHibernateSessionFactory();

		private static ISessionFactory _sessionFactory;

		protected override void EstablishContext()
		{
			InitSessionFactory();
			SchemaExport();
			ProvideImplementationOf(_sessionFactory);
			Session = _sessionFactory.OpenSession();
			Session.FlushMode = FlushMode.Commit;
			ProvideImplementationOf(Session);
		}

		private void SchemaExport()
		{
			_inMemoryNHibernateSessionFactory.SchemaExport();
		}

		protected TType Persist<TType>(TType entity)
		{
			WithinTransactionDo((s) => s.Save(entity));
			return entity;
		}

		protected void WithinTransactionDo(Action<ISession> doJob)
		{			
			using (var t = Session.BeginTransaction())
			{
				doJob(Session);
				t.Commit();
			}
			
			Session.Clear();
		}

		protected void WithEntityInDatabaseDo<TEntity>(int pk, Action<TEntity> doJob)
		{
			WithEntityInDatabaseDo<int,TEntity>(pk, doJob);
		}

		protected void WithEntityInDatabaseDo<TPk,TEntity>(TPk pk, Action<TEntity> doJob)
		{
			WithinTransactionDo(s=> doJob(s.Get<TEntity>(pk)));
		}

		protected ISession Session
		{
			get; private set;
		}

		private void InitSessionFactory()
		{
			if(_sessionFactory == null){}
				_sessionFactory = _inMemoryNHibernateSessionFactory.CreateSessionFactory();
		}

		protected override void AfterEachObservation()
		{
			try
			{
				Session.Dispose();
			}catch{}
			base.AfterEachObservation();
		}
	}
}