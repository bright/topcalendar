using Microsoft.Practices.Composite.Events;
using NUnit.Framework;
using Rhino.Mocks;
using TopCalendar.UI.Infrastructure;
using TopCalendar.Utility.Tests;
using TopCalendar.Utility.UI;
using Microsoft.Practices.Composite.Modularity;

namespace TopCalendar.UI.Modules.Registration.Tests
{
	public abstract class observations_for_removing_view
		: observations_for_auto_created_sut_of_type_with_eventaggregator<RegistrationModule>
	{
		protected UnloadModuleEvent moduleEvent;
		protected DeactivateViewEvent viewEvent;
		
		protected bool _unloadModuleExecuted;

		protected override void EstablishContext()
		{
			base.EstablishContext();
			moduleEvent = EventAggr.GetEvent<UnloadModuleEvent>();
			moduleEvent.Subscribe(execute_action);
			_unloadModuleExecuted = false;
			viewEvent = EventAggr.GetEvent<DeactivateViewEvent>();			
		}

		protected override void AfterSutCreation()
		{
			Sut.Initialize();
		}

		private void execute_action(IModule obj)
		{
			_unloadModuleExecuted = true;
		}
	}

	public class when_removing_registration_view : observations_for_removing_view
	{
		protected override void Because()
		{
			var view = GenerateStub<IRegistrationView>();
			viewEvent.Publish(view);
		}

		[Test]
		public void should_publish_unload_module_event()
		{
			_unloadModuleExecuted.ShouldBeTrue();
		}
	}

	public class when_removing_other_than_registration_view : observations_for_removing_view
	{
		protected override void Because()
		{			
			var view = Stub<IView>();
			viewEvent.Publish(view);
		}

		[Test]
		public void should_publish_unload_module_event()
		{
			_unloadModuleExecuted.ShouldBeFalse();
		}
	}
}
