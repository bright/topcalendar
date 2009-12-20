#region

using System;
using TopCalendar.Server.DataLayer.Entities;
using TopCalendar.Utility;

#endregion

namespace TopCalendar.Server.DataLayer.Specifications
{
    public class TaskSpecification
    {
    	public TaskSpecification()
    	{    		
    	}

    	public TaskSpecification(User user)
    	{
    		Check.Guard(user != null, "Cant create task specification without user");
    		User = user;
    	}

    	public DateTime? StartAtFrom { get; set; }
        public DateTime? StartAtTo { get; set; }
        public User User { get; set; }
    }
}