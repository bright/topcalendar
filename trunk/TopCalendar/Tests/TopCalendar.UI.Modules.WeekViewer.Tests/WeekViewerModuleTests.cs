using System;
using NUnit.Framework;
using TopCalendar.UI.Infrastructure;
using TopCalendar.UI.MenuInfrastructure;
using TopCalendar.Utility.Tests;
using TopCalendar.Utility.UI;
using Rhino.Mocks;

namespace TopCalendar.UI.Modules.WeekViewer.Tests
{

	public class when_week_viewer_module_adds_items_to_menu : observations_for_auto_created_sut_of_type_with_eventaggregator<WeekViewerModule>
	{
		protected override void Because()
		{
			Sut.Initialize();
		}

		[Test]
		public void should_add_item_to_menu()
		{
			Dependency<IMenuManager>()
				.AssertWasCalled(
					menu=> menu.AddItemToMenu<ShowWeekViewEvent, DateTime?>(Arg<string>.Is.NotNull,Arg<string>.Is.NotNull, Arg<string>.Is.NotNull, Arg<CommandCanExecuteHelper>.Is.NotNull)
				);
		}
	}


	public class when_week_viewer_module_registers_services : observations_for_auto_created_sut_of_type_with_eventaggregator<WeekViewerModule>
	{
		protected override void Because()
		{
			Sut.Initialize();
		}

		protected override void EstablishContext()
		{
			base.EstablishContext();
			MarkNonMocked<IWeekView>();
			MarkNonMocked<IPresentationModelFor<IWeekView>>();
			MarkNonMocked<IWeekTaskLoader>();
		}

		[Test]
		public void should_register_week_view()
		{
			IsTypeBinded<IWeekView>().ShouldBeTrue();
		}

		[Test]
		public void should_register_presentation_model()
		{
			IsTypeBinded<IPresentationModelFor<IWeekView>>().ShouldBeTrue();
		}

		[Test]
		public void should_register_week_task_loader()
		{
			IsTypeBinded<IWeekTaskLoader>().ShouldBeTrue();
		}
	}
}