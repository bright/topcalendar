using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Composite.Logging;
using Microsoft.Practices.Composite.Events;
using TopCalendar.UI.Infrastructure;

namespace TopCalendar.UI.PluginManager
{
	/// <summary>
	/// Nakladka na domyslnego CAL-owego inicjalizatora modulow, ktora zbiera instancje
	/// modulow na liscie (utrzymuje referencje), zeby zarzadzac czasem ich zycia
	/// w sposob bardziej przewidywalny niz domyslny
	/// </summary>
	public class PluginInitializer : ModuleInitializer
	{
		private readonly List<IModule> _loadedModules = new List<IModule>();

		public PluginInitializer(IServiceLocator serviceLocator, ILoggerFacade loggerFacade, IEventAggregator eventAggregator)
			: base(serviceLocator, loggerFacade)
		{
			eventAggregator.GetEvent<UnloadModuleEvent>().Subscribe(UnloadModule);
		}

		protected override IModule CreateModule(string typeName)
		{
			var instance = base.CreateModule(typeName);
			_loadedModules.Add(instance);
			return instance;
		}

		private void UnloadModule(IModule module)
		{
			_loadedModules.Remove(module);
		}
	}
}
