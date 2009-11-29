using Microsoft.Practices.Composite.Events;
using NUnit.Framework;
using Rhino.Mocks;
using TopCalendar.UI.Infrastructure;
using TopCalendar.Utility.Tests;
using TopCalendar.Utility.UI;

namespace TopCalendar.UI.Modules.Registration.Tests
{
	public class when_executing_cancel
		: observations_for_auto_created_sut_of_type<RegistrationPresentationModel>
	{
		private UnloadViewEvent subscriber;
		
		private bool _cancelActionExecuted;

		protected override void Because()
		{
			Sut.CancelCommand.Execute(null);
		}

		protected override void EstablishContext()
		{
			subscriber = new UnloadViewEvent();
			subscriber.Subscribe(execute_action);
			_cancelActionExecuted = false;	
			Dependency<IEventAggregator>()
				.Stub(ea => ea.GetEvent<UnloadViewEvent>())
				.IgnoreArguments()
				.Return(subscriber);
		}

		[Test]
		public void should_publish_unload_module_event()
		{
			_cancelActionExecuted.ShouldBeTrue();
		}

		private void execute_action(IView obj)
		{
			_cancelActionExecuted = true;
		}
	}
	
	public class when_filling_login_and_password
		: observations_for_auto_created_sut_of_type<RegistrationPresentationModel>
	{
		private RegistrationCompletedEvent regCompletedSubscriber;
		private UnloadViewEvent unloadSubscriber;


		private bool _viewShouldDieExecuted;
		private bool _registeredExecuted;

		protected override void EstablishContext()
		{
			unloadSubscriber = new UnloadViewEvent();			
			unloadSubscriber.Subscribe(execute_unload);

			regCompletedSubscriber = new RegistrationCompletedEvent();
			regCompletedSubscriber.Subscribe(execute_register);

			_viewShouldDieExecuted = false;

			Dependency<IEventAggregator>()
				.Stub(ea => ea.GetEvent<UnloadViewEvent>())
				.IgnoreArguments()
				.Return(unloadSubscriber);
			Dependency<IEventAggregator>()
				.Stub(ea => ea.GetEvent<RegistrationCompletedEvent>())
				.IgnoreArguments()
				.Return(regCompletedSubscriber);
		}		

		protected override void AfterSutCreation()
		{
			Sut.Login = "Login";
			Sut.Password = "Password";
		}
		
		protected override void Because()
		{		
			Sut.RegisterCommand.Execute(null);
		}			

		[Test]
		public void can_execute_register()
		{
			Sut.RegisterCommand.CanExecute(null).ShouldBeTrue();
		}

		[Test]
		public void should_publish_view_should_die()
		{
			_viewShouldDieExecuted.ShouldBeTrue();
		}

		[Test]
		public void should_publish_registration_completed()
		{
			_registeredExecuted.ShouldBeTrue();
		}

		private void execute_unload(IView obj)
		{
			_viewShouldDieExecuted = true;
		}

		private void execute_register(string str)
		{
			_registeredExecuted = true;
		}
	}


    public class when_getting_registration_view_from_presentation_model
		: observations_for_auto_created_sut_of_type<RegistrationPresentationModel>   
    {
    	private IRegistrationView RegisterViewFromPM;
    	private IRegistrationView RegisterView;

    	protected override void EstablishContext()
		{
			RegisterView = Stub<IRegistrationView>();
    		ProvideImplementationOf(RegisterView);			
		}

    	protected override void Because()
		{
			RegisterViewFromPM = Sut.View;
		}

        [Test]
        public void should_return_proper_view()
        {
            RegisterViewFromPM.ShouldNotBeNull();
			RegisterView.ShouldEqual(RegisterViewFromPM);
        }

        [Test]
        public void should_have_proper_view_model()
        {        	
        	RegisterViewFromPM.ViewModel.ShouldEqual(Sut);
        }

        [Test]
        public void should_have_proper_register_command()
        {
            Sut.RegisterCommand.ShouldNotBeNull();
        }

    	[Test]
    	public void register_command_should_not_be_excecutable()
    	{
    		Sut.RegisterCommand.CanExecute(null).ShouldBeFalse();
    	}

    }
    
}
