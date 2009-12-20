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
        public TasksRepository(ISession session)
            : base(session)
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
            
            var query = Session.CreateCriteria(typeof (Task));

            query.Add(Restrictions.Eq("User", taskSpecification.User));
            
            if (taskSpecification.StartAtFrom != null)
            {
                query.Add(Restrictions.Ge("StartAt", taskSpecification.StartAtFrom));   
            }

            if (taskSpecification.StartAtTo != null)
            {
                query.Add(Restrictions.Le("StartAt", taskSpecification.StartAtTo));
            }
       
            var results = query.List<Task>();
            return results;

        }
    }
}