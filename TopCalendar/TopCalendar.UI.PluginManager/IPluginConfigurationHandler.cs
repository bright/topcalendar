using System.Collections.Generic;

namespace TopCalendar.UI.PluginManager
{
	public interface IPluginConfigurationHandler
	{
		List<Plugin> Plugins { get; }
	}
}