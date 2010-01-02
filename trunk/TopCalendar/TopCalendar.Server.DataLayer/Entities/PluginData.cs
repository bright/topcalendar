using System;
using TopCalendar.Utility;

namespace TopCalendar.Server.DataLayer.Entities
{
	public class PluginData : DomainObject<Guid>
	{
		public virtual Task Task { get; private set; }

		protected PluginData()
		{			
		}

		public PluginData(Task task)
		{
			Check.Guard(task != null, "Can't create PluginData null task");
			Task = task;
		}
	}
}