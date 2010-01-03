using System;
using TopCalendar.Utility;

namespace TopCalendar.Server.DataLayer.Entities
{
	public class PluginData : DomainObject<int>
	{		
		public virtual Task Task { get; private set; }
		public virtual Guid PluginIdentifier { get; private set; }
		public virtual byte[] Data { get; set; }

		protected PluginData()
		{			
		}

		public PluginData(Task task, Guid pluginIdentifier)
		{		
			Check.Guard(task != null, "Can't create PluginData null task");
			Task = task;
			PluginIdentifier = pluginIdentifier;
		}
	}
}