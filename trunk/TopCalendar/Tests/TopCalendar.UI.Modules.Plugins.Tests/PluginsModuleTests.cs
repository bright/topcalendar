using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TopCalendar.Utility.Tests;
using TopCalendar.UI.Infrastructure;
using Microsoft.Practices.Composite.Events;
using NUnit.Framework;
using TopCalendar.UI.PluginManager;
using TopCalendar.Utility.UI;
using Rhino.Mocks;
using TopCalendar.UI.MenuInfrastructure;

namespace TopCalendar.UI.Modules.Plugins.Tests
{
	public class when_ShowPluginsEvent_is_published
		: observations_for_auto_created_sut_of_type_with_eventaggregator<PluginsModule>
	{
		private ShowPluginsEvent _showEvent;
		private IPluginsView _pluginsView;

		protected override void Because()
		{
			_showEvent.Publish(null);
		}

		protected override void AfterSutCreation()
		{
			Sut.Initialize();
		}

		protected override void EstablishContext()
		{
			base.EstablishContext();
			_showEvent = EventAggr.GetEvent<ShowPluginsEvent>();
			_pluginsView = Dependency<IPluginsView>();
			Dependency<IPluginsViewPresentationModel>().Stub(x => x.View).Return(_pluginsView);
		}

		[Test]
		public void should_acitevate_plugins_view()
		{
			Dependency<IPluginLoader>().AssertWasCalled(loader =>
				loader.ActivateView(
					Arg<string>.Is.Equal(RegionNames.MainContent),
					Arg<IView>.Is.NotNull
				)
			);
		}

	}

	public class when_PluginsModule_is_created
		: observations_for_auto_created_sut_of_type_with_eventaggregator<PluginsModule>
	{
		protected override void Because()
		{
			Sut.Initialize();
		}

		[Test]
		public void entry_should_be_added_to_program_menu()
		{
			Dependency<IMenuManager>().AssertWasCalled(mm =>
				mm.AddItemToMenu<ShowPluginsEvent>(
					Arg<string>.Is.Equal("Program"),
					Arg<string>.Is.Anything,
					Arg<string>.Is.Anything
				)
			);
		}

		[Test]
		public void should_register_plugins_view()
		{
			Dependency<IPluginLoader>().AssertWasCalled(pl=>
				pl.RegisterInActiveViewWithRegion(Arg<string>.Is.NotNull, Arg<Func<IView>>.Is.NotNull));
		}
	}
}
