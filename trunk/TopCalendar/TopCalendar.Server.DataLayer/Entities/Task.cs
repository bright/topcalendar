using System;
using System.Collections.Generic;
using TopCalendar.Utility;

namespace TopCalendar.Server.DataLayer.Entities
{
	public class Task : DomainObject<int>
    {
    	protected Task()
    	{    		
    	}

    	public Task(User user)
    	{
    		Check.Guard(user != null, "Cant create task without user");
    		User = user;
    	}  	

        public virtual string Name { get; set; }

        public virtual DateTime StartAt { get; set; }

        public virtual DateTime? FinishAt { get; set; }

        public virtual string Description { get; set; }

        public virtual User User { get; set; }

		public virtual IList<PluginData> PluginDatas { get; set; }
    }
}