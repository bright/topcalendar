using System;
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
		private IRegistrationView RegisterView;
		private ViewCompleted<IRegistrationView> subscriber;
		
		private bool _cancelActionExecuted;

		protected override void Because()
		{
			Sut.CancelCommand.Execute(null);
		}

		protected override void EstablishContext()
		{
			subscriber = new ViewCompleted<IRegistrationView>();
			subscriber.Subscribe(execute_action);
			_cancelActionExecuted = false;	
			Dependency<IEventAggregator>()
				.Stub(ea => ea.GetEvent<ViewCompleted<IRegistrationView>>())
				.IgnoreArguments()
				.Return(subscriber);
		}

		[Test]
		public void should_publish_view_should_die_event()
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
		private IRegistrationView RegisterView;
		private ViewCompleted<IRegistrationView> subscriber;
		private bool _viewShoulDieExecuted;

		protected override void EstablishContext()
		{
			subscriber = new ViewCompleted<IRegistrationView>();			
			subscriber.Subscribe(execute_action);
			_viewShoulDieExecuted = false;
			Dependency<IEventAggregator>()
				.Stub(ea => ea.GetEvent<ViewCompleted<IRegistrationView>>())
				.IgnoreArguments()
				.Return(subscriber);				
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
			_viewShoulDieExecuted.ShouldBeTrue();
		}

		private void execute_action(IView obj)
		{
			_viewShoulDieExecuted = true;
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
