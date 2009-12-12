using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Composite.Modularity;

namespace TopCalendar.UI.Modules.Plugins
{
	public class PluginInfo
	{
		public string Name { get; private set; }
		public string Type { get; private set; }
		public string Assembly { get; private set; }

		public PluginInfo(ModuleInfo module)
		{
			Name = module.ModuleName;
			Type = module.ModuleType;
			Assembly = module.Ref;
		}
	}
}
