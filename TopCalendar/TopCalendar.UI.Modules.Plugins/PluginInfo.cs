using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.ServiceLocation;
using System.Reflection;
using TopCalendar.UI.Modules.Plugins.Services;

namespace TopCalendar.UI.Modules.Plugins
{
	public class PluginInfo
	{
		public string Name { get; private set; }
		public string Type { get; private set; }
		public string Path { get; private set; }

		/// <summary>
		/// Tworzy obiekt reprezentujacy plugin za pomoca istniejacego, zaladowanego modulu
		/// </summary>
		/// <param name="module">Obiekt informacji o module</param>
		public PluginInfo(ModuleInfo module)
		{
			Name = module.ModuleName;
			Type = module.ModuleType;
			Path = new Uri(module.Ref).LocalPath;
		}

		/// <summary>
		/// Tworzy obiekt reprezentujacy plugin za pomoca sciezki do pliku
		/// </summary>
		/// <param name="path">Sciezka do dllki</param>
		public PluginInfo(string path)
		{
			var loader = new AssemblyLoader();
			var module = loader.Load(path);

			if (module == null)
			{
				throw new ArgumentException("Podany plik nie jest prawidłowym pluginem TopCalendar");
			}

			Name = module.Name;
			Type = module.FullName + ", " + module.Namespace;
			Path = path;
		}
	}
}
