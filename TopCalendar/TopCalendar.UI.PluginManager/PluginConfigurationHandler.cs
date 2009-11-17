using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Xml;

namespace TopCalendar.UI.PluginManager
{
	public class PluginConfigurationHandler : IConfigurationSectionHandler, IPluginConfigurationHandler
	{
		public List<Plugin> Plugins
		{
			get
			{
				try
				{
					return (List<Plugin>)ConfigurationManager.GetSection("PluginsConfiguration");
				}
				catch (ConfigurationException)
				{
					// @TODO powinformowac usera ze nie ma configa
					return new List<Plugin>();
				}
			}
		}

		public object Create(object parent, object configContext, XmlNode section)
		{
			var items = new List<Plugin>();
			XmlNodeList pluginsNodes = section.SelectNodes("Plugin");

			if (pluginsNodes != null)
			{
				var plugins = (from XmlNode item in pluginsNodes
					 where item.Attributes["Assembly"] != null
						&& item.Attributes["Active"] != null
						&& item.Attributes["Active"].InnerText == "True"
					 select new
			        	{
			        		Assembly = item.Attributes["Assembly"].InnerText,
							IsActive = true
			        	}); 

				foreach (var plugin in plugins)
				{
					items.Add(new Plugin() { Assembly = plugin.Assembly, IsActive = plugin.IsActive });
				}
			}

			return items;
		}
	}
}
