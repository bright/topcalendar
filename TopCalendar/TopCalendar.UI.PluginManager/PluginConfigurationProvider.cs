using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.Composite.Modularity;
using TopCalendar.Client.Connector;

namespace TopCalendar.UI.PluginManager
{
	public struct Plugin
	{
		public string Assembly { get; set; }
		public bool IsActive { get; set; }
	}

	public class PluginConfigurationProvider : IPluginConfigurationProvider
	{
		private readonly IPluginConfigurationHandler _handler;
		private readonly List<Type> _pluginsList = new List<Type>();

		public PluginConfigurationProvider(IPluginConfigurationHandler handler)
		{
			_handler = handler;
		}

		private void TryLoadPlugin(Plugin plugin)
		{
			if (File.Exists(plugin.Assembly))
			{
				try
				{
					var ass = Assembly.LoadFrom(plugin.Assembly);
					var plugins = (from item in ass.GetTypes()
								   where item.IsClass
										 && item.IsPublic
										 && item.GetInterfaces().Contains(typeof(IModule))
								   select item);
					if (plugins.Count() > 0)
					{
						_pluginsList.AddRange(plugins);
					}
				}
				catch (BadImageFormatException)
				{
					// @TODO zglosic userowi ze ma zlego plugina na liscie
				}
			}
		}

		public Type[] PresentationModules
		{
			get
			{
				foreach (var plugin in _handler.Plugins)
				{
					TryLoadPlugin(plugin);
				}

				return _pluginsList.ToArray();
			}
		}

		/// <summary>
		/// Zwraca liste modulow fasady klienckiej. Moduly nie sa dostepne do konfiguracji,
		/// bo ich lista nie jest konfigurowalna - wszystkie sa potrzebne i z gory okreslone.
		/// Sa jednak wyniesione do tej klasy, zeby je ladnie zamockowac :)
		/// </summary>
		public Type[] ConnectorModules
		{
			get
			{
				return new[]
			       	{
			       		typeof (IUserRegistrator)
			       	};
			}
		}
	}
}