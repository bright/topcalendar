using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace TopCalendar.UI.Modules.Plugins.Services
{
	public class ConfigurationXmlFactory
	{
		public XmlDocument CreateXML(IEnumerable<PluginInfo> list)
		{
			var xml = new XmlDocument();
			var root = xml.CreateElement("modules");

			foreach (var plugin in list)
			{
				var elem = xml.CreateElement("module");
				elem.SetAttribute("assemblyFile", plugin.Path);
				elem.SetAttribute("moduleType", plugin.Type);
				elem.SetAttribute("moduleName", plugin.Name);
				root.AppendChild(elem);
			}

			xml.AppendChild(root);
			return xml;
		}
	}
}
