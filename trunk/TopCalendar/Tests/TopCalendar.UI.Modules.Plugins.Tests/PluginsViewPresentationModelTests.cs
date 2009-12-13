using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TopCalendar.Utility.Tests;
using TopCalendar.UI.PluginManager;
using Microsoft.Practices.Composite.Modularity;
using NUnit.Framework;
using Rhino.Mocks;
using System.Windows.Input;
using Microsoft.Practices.Composite.Events;
using TopCalendar.UI.Infrastructure;
using TopCalendar.Utility.UI;

namespace TopCalendar.UI.Modules.Plugins.Tests
{
	public abstract class when_creating_PluginsViewPresentationModel
		: observations_for_auto_created_sut_of_type<PluginsViewPresentationModel>
	{
		protected List<ModuleInfo> _modules;

		protected virtual void InsertModules()
		{
		}

		protected override void EstablishContext()
		{
			_modules = new List<ModuleInfo>();
			InsertModules();

			Dependency<IPluginLoader>().Stub(pl => pl.ModuleCatalog.Modules).Return(_modules);	
		}

		protected override void Because()
		{
			// because creating
		}
	}

	public class when_creating_PluginsViewPresentationModel_without_plugins
		: when_creating_PluginsViewPresentationModel
	{
		[Test]
		public void plugins_list_should_be_empty()
		{
			Sut.PluginsList.ShouldBeEmpty();
		}
	}

	public class when_creating_PluginsViewPresentationModel_with_plugin
		: when_creating_PluginsViewPresentationModel
	{
		protected override void InsertModules()
		{
			var _module = new ModuleInfo()
			{
				ModuleName = "name",
				ModuleType = "type",
				Ref = "file:///c:/ref"
			};
			_modules.Add(_module); 
		}

		[Test]
		public void plugins_list_should_not_be_empty()
		{
			Sut.PluginsList.ShouldNotBeEmpty<PluginInfo>();
		}

		[Test]
		public void PluginInfo_object_should_have_correct_name()
		{
			Sut.PluginsList[0].Name.ShouldEqual("name");
		}

		[Test]
		public void PluginInfo_object_should_have_correct_type()
		{
			Sut.PluginsList[0].Type.ShouldEqual("type");
		}

		[Test]
		public void PluginInfo_object_should_have_correct_assembly()
		{
			Sut.PluginsList[0].Path.ShouldBeEqualIgnoringCase("c:\\ref");
		}
	}

	public class when_creating_PluginsViewPresentationModel_with_two_plugins
		: when_creating_PluginsViewPresentationModel
	{
		protected override void InsertModules()
		{
			var _module = new ModuleInfo()
			{
				ModuleName = "name1",
				ModuleType = "type1",
				Ref = "file:///c:/ref1"
			};
			_modules.Add(_module);

			_module = new ModuleInfo()
			{
				ModuleName = "name2",
				ModuleType = "type2",
				Ref = "file:///c:/ref2"
			};
			_modules.Add(_module);
		}

		[Test]
		public void plugins_list_should_have_count_2()
		{
			Sut.PluginsList.ShouldHaveCount<PluginInfo>(2);
		}

		[Test]
		public void PluginInfo_object_should_contain_first_object()
		{
			var result = (from item in Sut.PluginsList
						  where item.Name == "name1" && item.Type == "type1" && item.Path == "c:\\ref1"
						  select item);
			result.ShouldHaveCount<PluginInfo>(1);
		}

		[Test]
		public void PluginInfo_object_should_contain_second_object()
		{
			var result = (from item in Sut.PluginsList
						  where item.Name == "name2" && item.Type == "type2" && item.Path == "c:\\ref2"
						  select item);
			result.ShouldHaveCount<PluginInfo>(1);
		}
	}

	public class when_ui_is_cancelled
		: observations_for_auto_created_sut_of_type<PluginsViewPresentationModel>
	{
		private IView _viewToUnload;

		protected override void Because()
		{
			Sut.CancelCommand.Execute(null);
		}

		protected override void EstablishContext()
		{
			var unloadViewEvent = new UnloadViewEvent();
			unloadViewEvent.Subscribe(HandleEvent);

			Dependency<IEventAggregator>().Stub(aggregator =>
				aggregator.GetEvent<UnloadViewEvent>()).Return(unloadViewEvent);

			Dependency<IPluginLoader>().Stub(
				x => x.ModuleCatalog.Modules).Return(new List<ModuleInfo>()
			);
		}

		private void HandleEvent(IView view)
		{
			_viewToUnload = view;
		}

		[Test]
		public void plugins_view_should_be_unloaded()
		{
			_viewToUnload.ShouldEqual(Dependency<IPluginsView>());
		}
	}

	public class when_plugin_is_removed
		: observations_for_auto_created_sut_of_type<PluginsViewPresentationModel>
	{
		private PluginInfo _plugin;

		protected override void EstablishContext()
		{
			var _modules = new List<ModuleInfo>();
			Dependency<IPluginLoader>().Stub(pl => pl.ModuleCatalog.Modules).Return(_modules);
			
			_plugin = new PluginInfo(new ModuleInfo()
				{ ModuleName = "name", ModuleType = "type", Ref = "file:///c://ref" }
			);
		}

		protected override void AfterSutCreation()
		{
			Sut.PluginsList.Add(_plugin);
		}

		protected override void Because()
		{
			Sut.RemovePluginCommand.Execute(_plugin);
		}

		[Test]
		public void plugins_list_should_be_empty()
		{
			Sut.PluginsList.ShouldBeEmpty();
		}
	}
}
