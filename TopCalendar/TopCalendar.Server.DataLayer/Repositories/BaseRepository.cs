#region

using System;
using System.Collections.Generic;
using NHibernate;

#endregion

namespace TopCalendar.Server.DataLayer.Repositories
{
    public abstract class BaseRepository<T, TPk> : IRepository<T, TPk>
    {
        private readonly ISessionFactory _sessionFactory;

        protected BaseRepository(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public ISession GetSession()
        {
            return _sessionFactory.OpenSession();
        }

        public T GetById(TPk id)
        {
            using (var session = GetSession())
            {
                return session.Get<T>(id);
            }
        }

        public IList<T> GetAll()
        {
            using (var session = GetSession())
            {
                return session.CreateCriteria(typeof (T)).List<T>();
            }
        }

        public virtual T Add(T entity)
        {
            using (ISession session = GetSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                try
                {
                    session.Save(entity);
                    transaction.Commit();
                    return entity;
                }
                catch (Exception)
                {      
                   transaction.Rollback();
                   throw;
                }
            }
        }

        public void Remove(T entity)
        {
            using (ISession session = GetSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Delete(entity);
                transaction.Commit();
            }
        }
    }
}