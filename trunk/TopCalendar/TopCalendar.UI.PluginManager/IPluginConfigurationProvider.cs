using System;

namespace TopCalendar.UI.PluginManager
{
	public interface IPluginConfigurationProvider
	{
		Type[] PresentationModules { get; }
		Type[] ConnectorModules { get; }
	}
}