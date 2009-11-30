using FluentNHibernate.Mapping;
using TopCalendar.Server.DataLayer.Entities;

namespace TopCalendar.Server.DataLayer.Mappings
{
    public class TaskMap : ClassMap<Task>
    {
        public TaskMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Description);
            Map(x => x.StartAt);
            Map(x => x.FinishAt);
            References(x => x.User);
        }
    }
}