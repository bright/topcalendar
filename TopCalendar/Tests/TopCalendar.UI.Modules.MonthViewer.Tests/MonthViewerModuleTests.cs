using System;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Presentation.Regions;
using Microsoft.Practices.Composite.Regions;
using NUnit.Framework;
using Rhino.Mocks;
using TopCalendar.UI.Infrastructure;
using TopCalendar.UI.Modules.MonthViewer.Services;
using TopCalendar.UI.PluginManager;
using TopCalendar.Utility.Tests;
using TopCalendar.Utility.UI;

namespace TopCalendar.UI.Modules.MonthViewer.Tests
{

	public class when_raising_registration_completed_event_with_month_viewer 
		: observations_for_auto_created_sut_of_type_with_eventaggregator<MonthViewerModule>
	{
		private RegistrationCompletedEvent _registrationCompleted;


		protected override void Because()
		{
			_registrationCompleted.Publish(null);				
		}

		protected override void AfterSutCreation()
		{
			base.AfterSutCreation();
			Sut.Initialize();
		}

		protected override void EstablishContext()
		{
			base.EstablishContext();
			_registrationCompleted = EventAggr.GetEvent<RegistrationCompletedEvent>();						
		}

		[Test]
		public void should_register_month_view_in_main_region()
		{
			Dependency<IPluginLoader>().AssertWasCalled(loader=> loader.RegisterViewWithRegion(Arg.Is(RegionNames.MainContent), Arg<Func<IView>>.Is.NotNull));
		}
	}

	public class when_initializing_month_viewer_module
		: observations_for_auto_created_sut_of_type_with_eventaggregator<MonthViewerModule>
	{
		protected override void EstablishContext()
		{			
			base.EstablishContext();
			MarkNonMocked<IPresentationModelFor<IMonthView>>();
			MarkNonMocked<IMonthView>();
			MarkNonMocked<IMonthTaskLoader>();
		}

		protected override void Because()
		{
			Sut.Initialize();
		}

		[Test]
		public void should_bind_month_view()
		{
			IsTypeBinded<IMonthView>().ShouldBeTrue();
		}

		[Test]
		public void should_bind_month_view_presentation_model()
		{
			IsTypeBinded<IPresentationModelFor<IMonthView>>().ShouldBeTrue();
		}

		[Test]
		public void should_bind_month_task_loader()
		{
			IsTypeBinded<IMonthTaskLoader>().ShouldBeTrue();
		}
	}

	
}
