﻿using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using ServerLib.Domain;
using ServerLib.Helpers;

namespace ServerLib.Repositories
{
    /// <summary>
    /// Repozytorium obiektów typu BaseCalendarEntry.
    /// </summary>
    public class BaseCalendarEntryRepository : IBaseCalendarEntryRepository
    {
        #region IBaseCalendarEntryRepository Members

        public void Add(BaseCalendarEntry baseCalendarEntry)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Save(baseCalendarEntry);
                transaction.Commit();
            }
        }

        public void Update(BaseCalendarEntry baseCalendarEntry)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Update(baseCalendarEntry);
                transaction.Commit();
            }
        }
        public void WithTransactionDo(Action<ISession> doJob) 
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                doJob(session);
                transaction.Commit();
            }
        }
        public void Remove(BaseCalendarEntry baseCalendarEntry)
        {
            WithTransactionDo(s => s.Delete(baseCalendarEntry));
        }

        public BaseCalendarEntry FindById(Guid id)
        {
            using(ISession session = NHibernateHelper.OpenSession())
            {
                var result = session
                    .CreateCriteria(typeof (BaseCalendarEntry))
                    .Add(Restrictions.Eq("Id", id))
                    .UniqueResult<BaseCalendarEntry>();
                return result;
            }
        }

        public IList<BaseCalendarEntry> FindAll()
        {            
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var records = session
                    .CreateCriteria(typeof(BaseCalendarEntry))                    
                    .List<BaseCalendarEntry>();
                return records;
            }
        }


        public IList<BaseCalendarEntry> FindByDay(int year, int month, int day)
        {
            DateTime startOfDay = DateTimeHelper.StartOfDay(year, month, day);
            DateTime endOfDay = DateTimeHelper.EndOfDay(year, month, day);

            using(ISession session = NHibernateHelper.OpenSession())
            {
                var records = session
                    .CreateCriteria(typeof (BaseCalendarEntry))
                    .Add(Restrictions.Ge("DateTime", startOfDay))
                    .Add(Restrictions.Le("DateTime", endOfDay))
                    .List<BaseCalendarEntry>();
                return records;
            }
        }

        public IList<BaseCalendarEntry> FindBetweenDates(DateTime from, DateTime to)
        {            
            using(ISession session= NHibernateHelper.OpenSession())
            {
                return session
                    .CreateCriteria(typeof(BaseCalendarEntry))
                    .Add(Restrictions.Ge("DateTime", from))
                    .Add(Restrictions.Le("DateTime", to))
                    .List<BaseCalendarEntry>();
            }
        }

        #endregion
    }
}