using System.Linq;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Presentation.Regions;
using Microsoft.Practices.Composite.Regions;
using Ninject;
using TopCalendar.UI.Infrastructure;
using TopCalendar.Utility.UI;

namespace TopCalendar.UI.PluginManager
{
	public class PluginLoader : IPluginLoader
	{
		private readonly IKernel _kernel;
		private readonly IEventAggregator _eventAggregator;
		private readonly IRegionManager _regionManager;
		private readonly IPluginConfigurationProvider _configuration;

		public PluginLoader(IKernel kernel, IEventAggregator eventAggregator, IRegionManager regionManager)
		{
			_kernel = kernel;
			_kernel.Bind<IPluginConfigurationProvider>().To<PluginConfigurationProvider>();
			_kernel.Bind<IPluginConfigurationHandler>().To<PluginConfigurationHandler>();

			_eventAggregator = eventAggregator;
			_regionManager = regionManager;

			_configuration = _kernel.Get<IPluginConfigurationProvider>();

			_eventAggregator.GetEvent<UnloadModuleEvent>().Subscribe(UnloadModuleView);
		}

		/// <summary>
		/// Wywala podany widok z wszystkich regionow, w ktorych jest zarejestrowany
		/// </summary>
		/// <param name="moduleView">Widok do wywalenia</param>
		private void UnloadModuleView(IView moduleView)
		{
			var regions = from item in _kernel.Get<IRegionManager>().Regions
			              where item.Views.Contains(moduleView)
			              select item;

			foreach (var region in regions)
			{
				region.Remove(moduleView);
			}
		}

		/// <summary>
		/// Laduje moduly wg konfiguracji
		/// </summary>
		/// <param name="moduleCatalog"></param>
		public void Load(ModuleCatalog moduleCatalog)
		{
			LoadNinjectModules();
			LoadPresentationModules(moduleCatalog);
		}
		
		/// <summary>
		/// Laduje moduly (Microsoft.Practices.Composite.Modularity.IModule) UI do katalogu
		/// </summary>
		/// <param name="moduleCatalog">Katalog modulow, sluzy do ich inicjalizacji</param>
		private void LoadPresentationModules(ModuleCatalog moduleCatalog)
		{
			foreach (var module in _configuration.PresentationModules)
			{
				moduleCatalog.AddModule(module);
			}
		}

		/// <summary>
		/// Laduje moduly (Ninject.Module) fasady klienckiej.
		/// </summary>
		private void LoadNinjectModules()
		{
			foreach (var module in _configuration.ConnectorModules)
			{
				_kernel.Load(module.Assembly);
			}
		}

		/// <summary>
		/// Dodaje widok do regionu (wyswietla w UI), w razie potrzeby tworzy region
		/// </summary>
		/// <param name="regionName">Nazwa regionu. Regiony sa zdefiniowane w RegionNames</param>
		/// <param name="view">Obiekt widoku</param>
		public void RegisterViewWithRegion(string regionName, IView view)
		{
			if (!_regionManager.Regions.ContainsRegionWithName(regionName))
			{
				_regionManager.Regions.Add(new Region() { Name = regionName });
			}

			_regionManager.AddToRegion(regionName, view);
		}
	}
}
