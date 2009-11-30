#region

using System;
using TopCalendar.Server.DataLayer.Entities;

#endregion

namespace TopCalendar.Server.DataLayer.Specifications
{
    public class TaskSpecification
    {
        public DateTime? StartAtFrom { get; set; }
        public DateTime? StartAtTo { get; set; }
        public User User { get; set; }
    }
}