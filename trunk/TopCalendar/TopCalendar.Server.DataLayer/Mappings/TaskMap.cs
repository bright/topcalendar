using FluentNHibernate.Mapping;
using TopCalendar.Server.DataLayer.Entities;

namespace TopCalendar.Server.DataLayer.Mappings
{
    public class TaskMap : ClassMap<Task>
    {
        public TaskMap()
        {
        	Id(x => x.Id).Not.Nullable().Unique();
            Map(x => x.Name).Not.Nullable();
            Map(x => x.Description);
            Map(x => x.StartAt).Not.Nullable();
            Map(x => x.FinishAt);
            References(x => x.User).Not.Nullable();
        	HasMany(x => x.PluginDatas).Inverse().Cascade.AllDeleteOrphan();
        }
    }
}