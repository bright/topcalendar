using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Presentation.Regions;
using Microsoft.Practices.Composite.Regions;
using Ninject;
using TopCalendar.UI.Infrastructure;
using TopCalendar.Utility.UI;
using Microsoft.Practices.ServiceLocation;

namespace TopCalendar.UI.PluginManager
{
	public class PluginLoader : IPluginLoader
	{
		private readonly IKernel _kernel;
		private readonly IEventAggregator _eventAggregator;
		private readonly IRegionManager _regionManager;
		private readonly IConnectorsListProvider _connectors;

		private IModuleCatalog _moduleCatalog;

		public PluginLoader(IKernel kernel, IEventAggregator eventAggregator, IRegionManager regionManager)
		{
			_kernel = kernel;
			_kernel.Bind<IConnectorsListProvider>().To<ConnectorsListProvider>();

			_eventAggregator = eventAggregator;
			_regionManager = regionManager;

			_connectors = _kernel.Get<IConnectorsListProvider>();

			_eventAggregator.GetEvent<DeactivateViewEvent>().Subscribe(DeactivateView);
		}

		/// <summary>
		/// Udostepnia ModuleCatalog tworzony na podstawie konfiguracji
		/// </summary>
		public IModuleCatalog ModuleCatalog
		{
			get
			{
				if (_moduleCatalog == null)
				{
					_moduleCatalog = new ConfigurationModuleCatalog();
				}

				return _moduleCatalog;
			}
		}
        
		private void DeactivateView(IView moduleView)
		{
			var regions = GetRegionsContaningView(moduleView);

			foreach (var region in regions)
			{
				region.Deactivate(moduleView);
			}
		}

		private IEnumerable<IRegion> GetRegionsContaningView(IView moduleView)
		{
			return from item in _kernel.Get<IRegionManager>().Regions
			       where item.Views.Contains(moduleView)
			       select item;
		}

		/// <summary>
		/// Laduje moduly (Ninject.Module) fasady klienckiej.
		/// </summary>
		public void LoadConnectors()
		{
			foreach (var module in _connectors.ConnectorModules)
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

		public void RegisterViewWithRegion(string regionName, Func<IView> viewProvider)
		{
			RegisterViewWithRegion(regionName, viewProvider());
		}

		public void ActivateView(string regionName, IView view)
		{
			var region = _regionManager.Regions[regionName];
			if(region == null)
				return;
			region.Activate(view);
		}

		public void ActivateView(string regionName, Func<IView> viewProvider)
		{
			ActivateView(regionName,viewProvider());
		}

		public void RegisterInActiveViewWithRegion(string regionName, IView view)
		{
			RegisterViewWithRegion(regionName, view);
			_regionManager.Regions[regionName].Deactivate(view);
		}

		public void RegisterInActiveViewWithRegion(string regionName, Func<IView> view)
		{
			RegisterInActiveViewWithRegion(regionName, view());
		}
	}
}
