using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TopCalendar.Utility.Tests;
using TopCalendar.UI.Modules.Plugins.Services;
using System.Xml;
using NUnit.Framework;
using Microsoft.Practices.Composite.Modularity;

namespace TopCalendar.UI.Modules.Plugins.Tests
{
	public abstract class observations_for_ConfigurationXmlFactory
		: observations_for_auto_created_sut_of_type<ConfigurationXmlFactory>
	{
		protected List<PluginInfo> _list;
		protected XmlElement _xml;

		protected override void EstablishContext()
		{
			_list = new List<PluginInfo>();
		}

		protected override void Because()
		{
			_xml = Sut.CreateXML(_list).ChildNodes[0] as XmlElement;
		}
	}

	public class when_plugins_list_is_empty
		: observations_for_ConfigurationXmlFactory
	{
		[Test]
		public void xml_should_be_empty()
		{
			(_xml.SelectSingleNode("module") as XmlElement).ShouldBeNull();
		}
	}

	public class when_plugins_list_has_one_element
		: observations_for_ConfigurationXmlFactory
	{
		private PluginInfo _plugin;

		protected override void EstablishContext()
		{
			base.EstablishContext();

			_plugin = new PluginInfo(new ModuleInfo()
				{
					ModuleName = "name",
					ModuleType = "type",
					Ref = "file:///c:/ref" 
				}
			);
			_list.Add(_plugin);
		}

		[Test]
		public void xml_should_not_be_empty()
		{
			_xml.ShouldHaveChild("module");
		}
		
		[Test]
		public void xml_child_should_have_assemblyFile_attribute()
		{
			var node = _xml.SelectSingleNode("module");
			node.AttributeShouldEqual("assemblyFile", "c:\\ref");
		}

		[Test]
		public void xml_child_should_have_moduleType_attribute()
		{
			var node = _xml.SelectSingleNode("module");
			node.AttributeShouldEqual("moduleType", "type");
		}

		[Test]
		public void xml_child_should_have_moduleName_attribute()
		{
			var node = _xml.SelectSingleNode("module");
			node.AttributeShouldEqual("moduleName", "name");
		}
	}
}
