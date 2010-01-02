#region

using FluentNHibernate.Mapping;
using TopCalendar.Server.DataLayer.Entities;

#endregion

namespace TopCalendar.Server.DataLayer.Mappings
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(x => x.Id).Unique();
            Map(x => x.Login).Not.Nullable().Unique();
            Map(x => x.Password).Not.Nullable();			
        }
    }
}