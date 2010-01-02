using System;
using Microsoft.Practices.Composite.Presentation.Regions;
using Microsoft.Practices.Composite.Regions;
using NUnit.Framework;
using TopCalendar.UI.Infrastructure;
using TopCalendar.Utility.Tests;
using TopCalendar.Utility.UI;

namespace TopCalendar.UI.PluginManager.Tests
{
	public abstract class observations_for_PluginLoader
		: observations_for_auto_created_sut_of_type_with_eventaggregator<PluginLoader>
	{
		protected DeactivateViewEvent DeactivateEvent;
		protected IRegionManager _regionManager;

		protected string _testRegion = "testRegion";
		protected IView _view;

		protected override void EstablishContext()
		{
			base.EstablishContext();
			DeactivateEvent = EventAggr.GetEvent<DeactivateViewEvent>();
			_view = Dependency<IView>();
			_regionManager = new RegionManager();			
			ProvideImplementationOf(_regionManager);			
		}
	}

	public class when_publishing_deactivate_view_event : observations_for_PluginLoader
	{
		protected override void AfterSutCreation()
		{
			Sut.RegisterViewWithRegion(_testRegion, _view);
		}

		protected override void Because()
		{
			DeactivateEvent.Publish(_view);
		}

		[Test]
		public void should_deactivate_view()
		{
			_regionManager.Regions[_testRegion].ActiveViews.ShouldNotContain(_view);
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

	public class when_registering_inactive_view : observations_for_PluginLoader
	{
		protected override void Because()
		{
			Sut.RegisterInActiveViewWithRegion(_testRegion, ()=> _view);
		}

		[Test]
		public void should_add_view_to_region()
		{
			_regionManager.Regions[_testRegion].Views.ShouldContain(_view);
		}

		[Test]
		public void should_mark_view_as_inactive()
		{
			_regionManager.Regions[_testRegion].ActiveViews.ShouldNotContain(_view);
		}
	}

	public class when_activateing_view : observations_for_PluginLoader
	{
		protected override void Because()
		{
			Sut.ActivateView(_testRegion,()=>_view);
		}

		protected override void AfterSutCreation()
		{
			Sut.RegisterInActiveViewWithRegion(_testRegion,_view);
		}

		[Test]
		public void should_mark_view_as_active()
		{
			_regionManager.Regions[_testRegion].ActiveViews.ShouldContain(_view);
		}
	}
}
