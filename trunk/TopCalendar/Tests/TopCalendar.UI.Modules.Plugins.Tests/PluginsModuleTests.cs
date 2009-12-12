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
		: observations_for_auto_created_sut_of_type<PluginsModule>
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
			IEventAggregator eventAggregator = Dependency<IEventAggregator>();

			_showEvent = new ShowPluginsEvent();

			eventAggregator.Stub(aggregator =>
				aggregator.GetEvent<ShowPluginsEvent>()).Return(_showEvent);

			_pluginsView = Dependency<IPluginsView>();
			Dependency<IPluginsViewPresentationModel>().Stub(x => x.View).Return(_pluginsView);
		}

		[Test]
		public void plugins_view_should_be_registered_with_MainContent_region()
		{
			Dependency<IPluginLoader>().AssertWasCalled(loader =>
				loader.RegisterViewWithRegion(
					Arg<string>.Is.Equal(RegionNames.MainContent),
					Arg<IView>.Is.Equal(_pluginsView)
				)
			);
		}
	}

	public class when_PluginsModule_is_created
		: observations_for_auto_created_sut_of_type<PluginsModule>
	{
		protected override void Because()
		{
			Sut.Initialize();
		}

		protected override void EstablishContext()
		{
			Dependency<IEventAggregator>().Stub(aggregator =>
				aggregator.GetEvent<ShowPluginsEvent>()).Return(new ShowPluginsEvent());
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
	}
}
