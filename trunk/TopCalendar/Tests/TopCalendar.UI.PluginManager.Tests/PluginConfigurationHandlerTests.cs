using System.Collections.Generic;
using System.Xml;
using NUnit.Framework;
using TopCalendar.Utility.Tests;

namespace TopCalendar.UI.PluginManager.Tests
{
	public abstract class observations_for_ConfigurationHandler
		: observations_for_auto_created_sut_of_type<PluginConfigurationHandler>
	{
		protected List<Plugin> _result;
		protected XmlNode _xml;
		protected string _testXml;

		protected string _test1Assembly = "test1";
		protected string _test2Assembly = "test2";
        
		protected abstract void CreateXml();

		protected override void EstablishContext()
		{
			CreateXml();

			var fragment = new XmlDocument().CreateDocumentFragment();
			fragment.InnerXml = _testXml;
			_xml = fragment.FirstChild;
		}
		
		protected override void Because()
		{
			_result = (List<Plugin>)Sut.Create(null, null, _xml);
		}
	}

	public class when_reading_configuration_case1 : observations_for_ConfigurationHandler
	{
		protected override void CreateXml()
		{
			_testXml = "<PluginsConfiguration><Plugin Assembly=\"" + _test1Assembly + "\" /></PluginsConfiguration>";
		}

		[Test]
		public void should_read_no_plugins()
		{
			_result.ShouldBeEmpty();
		}
	}

	public class when_reading_configuration_case2 : observations_for_ConfigurationHandler
	{
		protected override void CreateXml()
		{
			_testXml = "<PluginsConfiguration><Plugin Assembly=\"" + _test1Assembly + "\" Active=\"True\" /></PluginsConfiguration>";
		}

		[Test]
		public void should_read_one_plugin()
		{
			_result.ShouldHaveCount(1);
		}

		[Test]
		public void should_read_active_test1_plugin()
		{
			_result.ShouldContain(p => p.Assembly == _test1Assembly && p.IsActive);
		}
	}

	public class when_reading_configuration_case3 : observations_for_ConfigurationHandler
	{
		protected override void CreateXml()
		{
			_testXml = "<PluginsConfiguration><Plugin Assembly=\"" + _test1Assembly + "\" Active=\"True\" /><Plugin Assembly=\"" + _test2Assembly + "\" /></PluginsConfiguration>";
		}

		[Test]
		public void should_read_one_plugin()
		{
			_result.ShouldHaveCount(1);
		}

		[Test]
		public void should_read_active_test1_plugin()
		{
			_result.ShouldContain(p => p.Assembly == _test1Assembly && p.IsActive);
		}
	}

	public class when_reading_configuration_case4 : observations_for_ConfigurationHandler
	{
		protected override void CreateXml()
		{
			_testXml = "<PluginsConfiguration><Plugin Assembly=\"" + _test1Assembly + "\" IsActive=\"zażółć\" /></PluginsConfiguration>";
		}

		[Test]
		public void should_read_no_plugins()
		{
			_result.ShouldBeEmpty();
		}
	}

	public class when_reading_configuration_case5 : observations_for_ConfigurationHandler
	{
		protected override void CreateXml()
		{
			_testXml = "<PluginsConfiguration></PluginsConfiguration>";
		}

		[Test]
		public void should_read_no_plugins()
		{
			_result.ShouldBeEmpty();
		}
	}

	public class when_reading_configuration_case6 : observations_for_ConfigurationHandler
	{
		protected override void CreateXml()
		{
			_testXml = "<PluginsConfiguration><Plugin Assembly=\"" + _test1Assembly + "\" Active=\"True\" /><Plugin Assembly=\"" + _test2Assembly + "\" Active=\"True\" /></PluginsConfiguration>";
		}

		[Test]
		public void should_read_two_plugins()
		{
			_result.ShouldHaveCount(2);
		}

		[Test]
		public void should_read_active_test1_plugin()
		{
			_result.ShouldContain(p => p.Assembly == _test1Assembly && p.IsActive);
		}

		[Test]
		public void should_read_active_test2_plugin()
		{
			_result.ShouldContain(p => p.Assembly == _test2Assembly && p.IsActive);
		}
	}
}