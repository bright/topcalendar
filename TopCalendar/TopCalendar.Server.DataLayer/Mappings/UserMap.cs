using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using TopCalendar.Server.DataLayer.Entities;

namespace TopCalendar.Server.DataLayer.Mappings
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(x => x.Id);
            Map(x => x.Login);
            Map(x => x.Password);
        }
    }
}
