using System;

namespace TopCalendar.UI.PluginManager
{
	public interface IConnectorsListProvider
	{
		Type[] ConnectorModules { get; }
	}
}