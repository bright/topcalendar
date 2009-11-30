using System.Collections.Generic;
using TopCalendar.Server.DataLayer.Entities;
using TopCalendar.Server.DataLayer.Specifications;

namespace TopCalendar.Server.DataLayer.Repositories
{
    public interface ITasksRepository : IRepository<Task, int>
    {
        IList<Task> Find(TaskSpecification taskSpecification);
    }
}