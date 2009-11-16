using Microsoft.Practices.Composite.Modularity;
using TopCalendar.Utility.UI;

namespace TopCalendar.UI.PluginManager
{
	public interface IPluginLoader
	{
		void Load(ModuleCatalog moduleCatalog);

		void RegisterViewWithRegion(string regionName, IView view);
	}
}