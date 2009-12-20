#region

using System;
using System.Collections.Generic;
using NHibernate;

#endregion

namespace TopCalendar.Server.DataLayer.Repositories
{
    public abstract class BaseRepository<TEntity, TPk> : IRepository<TEntity, TPk>
		where TEntity : class 
    {
    	private readonly ISession _session;
    	

        protected BaseRepository(ISession session)
        {
        	_session = session;
        
        }

    	public ISession Session
    	{
			get { return _session; }
    	}


        public TEntity GetById(TPk id)
        {
            return Session.Get<TEntity>(id);            
        }

        public IList<TEntity> GetAll()
        {            
            return Session.CreateCriteria(typeof (TEntity)).List<TEntity>();            
        }

        public virtual TEntity Add(TEntity entity)
        {            
            Session.Save(entity);
        	return entity;
        }

        public void Remove(TEntity entity)
        {
            Session.Delete(entity);             
        }

		public virtual ICriteria CreateCriteria()
		{
			return Session.CreateCriteria<TEntity>();
		}
    }
}