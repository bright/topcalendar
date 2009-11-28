using Microsoft.Practices.Composite.Modularity;
using TopCalendar.Utility.UI;

namespace TopCalendar.UI.PluginManager
{
	public interface IPluginLoader
	{
		IModuleCatalog ModuleCatalog { get; }

		void LoadConnectors();

		void RegisterViewWithRegion(string regionName, IView view);
	}
}