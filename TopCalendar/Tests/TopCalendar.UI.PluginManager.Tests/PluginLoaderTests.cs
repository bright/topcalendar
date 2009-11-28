using System;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Presentation.Regions;
using Microsoft.Practices.Composite.Regions;
using Ninject;
using NUnit.Framework;
using Rhino.Mocks;
using TopCalendar.UI.Infrastructure;
using TopCalendar.Utility.Tests;
using TopCalendar.Utility.UI;

namespace TopCalendar.UI.PluginManager.Tests
{
	public abstract class observations_for_PluginLoader
		: observations_for_auto_created_sut_of_type<PluginLoader>
	{
		protected UnloadViewEvent _unloadEvent;
		protected IRegionManager _regionManager;

		protected string _testRegion = "testRegion";
		protected IView _view;

		protected override void EstablishContext()
		{
			_unloadEvent = new UnloadViewEvent();
			_view = Dependency<IView>();

			Kernel.Bind<IRegionManager>().To<RegionManager>().InSingletonScope();

			_regionManager = Kernel.Get<IRegionManager>();

			Dependency<IEventAggregator>().Stub(
				ea => ea.GetEvent<UnloadViewEvent>()
			).IgnoreArguments().Return(_unloadEvent);
		}
	}

	public class when_publishing_unload_module_event : observations_for_PluginLoader
	{
		protected override void AfterSutCreation()
		{
			Sut.RegisterViewWithRegion(_testRegion, _view);
		}

		protected override void Because()
		{
			_unloadEvent.Publish(_view);
		}

		[Test]
		public void should_remove_view_from_region()
		{
			_regionManager.Regions[_testRegion].Views.Contains(_view).ShouldBeFalse();
		}
	}

	public class when_adding_view_to_region : observations_for_PluginLoader
	{
		protected override void Because()
		{
			Sut.RegisterViewWithRegion(_testRegion, _view);
		}

		[Test]
		public void region_should_be_added()
		{
			_regionManager.Regions.ContainsRegionWithName(_testRegion).ShouldBeTrue();
		}

		[Test]
		public void view_should_be_added_to_region()
		{
			_regionManager.Regions[_testRegion].Views.Contains(_view).ShouldBeTrue();
		}
	}
}
