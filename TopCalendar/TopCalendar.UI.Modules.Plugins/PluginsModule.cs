using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Composite.Modularity;
using Ninject;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Logging;
using TopCalendar.UI.PluginManager;
using TopCalendar.UI.Infrastructure;
using TopCalendar.Utility.UI;
using TopCalendar.UI.MenuInfrastructure;
using Microsoft.Practices.Composite.Regions;

namespace TopCalendar.UI.Modules.Plugins
{
	public class PluginsModule : IModule
	{
		private readonly IKernel _kernel;
		private readonly IEventAggregator _eventAggregator;

		public PluginsModule(IKernel kernel, IEventAggregator eventAggregator)
		{
			_kernel = kernel;
			_eventAggregator = eventAggregator;
		}

		public void Initialize()
		{
			_kernel.Get<IMenuManager>().AddItemToMenu<ShowPluginsEvent>(
				"Program", "Plugins", "Manager pluginów"
			);

			RegisterViewsAndServices();
			SubscribeToDefaultEvents();
		}

		private void SubscribeToDefaultEvents()
		{
			_eventAggregator.GetEvent<ShowPluginsEvent>().Subscribe(LoadPluginsView);
		}

		private void LoadPluginsView(object obj)
		{
			_kernel.Get<IPluginLoader>().ActivateView(
				RegionNames.MainContent, _kernel.Get<IPluginsView>()
			);
		}

		private void RegisterViewsAndServices()
		{			
			_kernel.Bind<IPluginsView>().To<PluginsView>().InSingletonScope();
			_kernel.Bind<IPresentationModelFor<IPluginsView>>().To<PluginsViewPresentationModel>().InSingletonScope();
			_kernel.Bind<IViewForModel<IPluginsView, PluginsViewPresentationModel>>().To<PluginsView>().InSingletonScope();
			_kernel.Bind<IPluginsViewPresentationModel>().To<PluginsViewPresentationModel>().InSingletonScope();
			_kernel.Get<IPluginLoader>().RegisterInActiveViewWithRegion(
				RegionNames.MainContent,
				() => _kernel.Get<IPluginsViewPresentationModel>().View
			);
		}
	}
}
