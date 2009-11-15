using Microsoft.Practices.Composite.Modularity;

namespace TopCalendar.UI.PluginManager
{
	public interface IPluginLoader
	{
		void Load(ModuleCatalog moduleCatalog);
	}
}