using System;
using Microsoft.Practices.Composite.Modularity;
using TopCalendar.Utility.UI;

namespace TopCalendar.UI.PluginManager
{
	public interface IPluginLoader
	{
		IModuleCatalog ModuleCatalog { get; }

		void LoadConnectors();

		void RegisterViewWithRegion(string regionName, IView view);

		void RegisterViewWithRegion(string regionName, Func<IView> viewProvider);
		void ActivateView(string regionName, IView view);
		void ActivateView(string regionName, Func<IView> viewProvider);
		void RegisterInActiveViewWithRegion(string regionName, IView view);
		void RegisterInActiveViewWithRegion(string regionName, Func<IView> view);
	}
}