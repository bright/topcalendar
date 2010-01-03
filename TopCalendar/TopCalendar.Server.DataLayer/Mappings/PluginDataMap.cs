using FluentNHibernate.Mapping;
using TopCalendar.Server.DataLayer.Entities;

namespace TopCalendar.Server.DataLayer.Mappings
{
	public class PluginDataMap : ClassMap<PluginData>
	{
		public PluginDataMap()
		{
			Id(x => x.Id).Not.Nullable().Unique();
			Map(x => x.PluginIdentifier).Not.Nullable();
			Map(x => x.Data);
			References(x => x.Task).Not.Nullable();
		}
	}
}