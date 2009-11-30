#region

using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Mapping;
using TopCalendar.Server.DataLayer.Entities;
using TopCalendar.Server.DataLayer.Specifications;
using TopCalendar.Utility;

#endregion

namespace TopCalendar.Server.DataLayer.Repositories
{
    public class TasksRepository : BaseRepository<Task, int>, ITasksRepository
    {
        public TasksRepository(ISessionFactory sessionFactory)
            : base(sessionFactory)
        {
        }

        public override Task Add(Task entity)
        {
            Check.Guard(entity.User != null, "Task without User");
            return base.Add(entity);
        }

        public IList<Task> Find(TaskSpecification taskSpecification)
        {
            Check.Guard(taskSpecification.User != null, "TaskSpecification without user");

            using (var session = GetSession())
            {
                var query = session.CreateCriteria(typeof (Task));

                query.Add(Expression.Eq("User", taskSpecification.User));
                
                if (taskSpecification.StartAtFrom != null)
                {
                    query.Add(Expression.Ge("StartAt", taskSpecification.StartAtFrom));   
                }

                if (taskSpecification.StartAtTo != null)
                {
                    query.Add(Expression.Le("StartAt", taskSpecification.StartAtTo));
                }
           
                var results = query.List<Task>();
                return results;
            }
        }
    }
}