using System;
using System.Collections.Generic;
using TopCalendar.Utility;

namespace TopCalendar.Server.DataLayer.Entities
{
	public class Task : DomainObject<int>
    {
    	protected Task()
    	{
    		PluginDatas = new List<PluginData>();
    	}

		public Task(User user)
    	{			
			Check.Guard(user != null, "Cant create task without user");
    		User = user;
			PluginDatas = new List<PluginData>();
    	}  	

        public virtual string Name { get; set; }

        public virtual DateTime StartAt { get; set; }

        public virtual DateTime? FinishAt { get; set; }

        public virtual string Description { get; set; }

        public virtual User User { get; set; }

		public virtual IList<PluginData> PluginDatas { get; protected set; }

		public virtual void AddPluginData(Guid pluginIdentifier, byte[] data)
		{
			var pd = new PluginData(this, pluginIdentifier)
			         	{
			         		Data = data
			         	};
			PluginDatas.Add(pd);
		}

		public virtual void RemovePluginData(PluginData data)
		{
			PluginDatas.Remove(data);
		}
    }
}