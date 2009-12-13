using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows;
using System.IO;

namespace TopCalendar.UI.Modules.Plugins.Services
{
	public class PluginsExporter
	{
		public void Export(IEnumerable<PluginInfo> list, string configFile)
		{
			var xmlGenerator = new ConfigurationXmlFactory();
			var xml = xmlGenerator.CreateXML(list);

			xml.Save(Application.Current.Properties["workingDir"] + "\\" + configFile);
		}
	}
}
