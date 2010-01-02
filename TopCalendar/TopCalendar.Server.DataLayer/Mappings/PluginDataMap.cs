using FluentNHibernate.Mapping;
using TopCalendar.Server.DataLayer.Entities;

namespace TopCalendar.Server.DataLayer.Mappings
{
	public class PluginDataMap : ClassMap<PluginData>
	{
		public PluginDataMap()
		{
			Id(x => x.Id).GeneratedBy.Assigned().Unique().Not.Nullable();
			References(x => x.Task).Not.Nullable();
		}
	}
}