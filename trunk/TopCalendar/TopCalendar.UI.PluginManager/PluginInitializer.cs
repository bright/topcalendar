using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Composite.Logging;

namespace TopCalendar.UI.PluginManager
{
	public class PluginInitializer : ModuleInitializer
	{
		public List<IModule> LoadedModules { get; set; }

		public PluginInitializer(IServiceLocator serviceLocator, ILoggerFacade loggerFacade)
			: base(serviceLocator, loggerFacade)
		{
			LoadedModules = new List<IModule>();
		}

		protected override IModule CreateModule(string typeName)
		{
			var instance = base.CreateModule(typeName);
			LoadedModules.Add(instance);
			return instance;
		}
	}
}
