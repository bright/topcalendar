using NUnit.Framework;
using TopCalendar.Utility.Tests;

namespace TopCalendar.UI.Modules.Registration.Tests
{
    public class when_getting_registration_view_from_presenter
        : observations_for_auto_created_sut_of_type<RegistrationPresenter>
    {
        private IRegistrationView _registerView;
        private RegistrationPresentationModel _viewModel;


        protected override void EstablishContext()
        {
            Stub<IRegistrationView>();
        }


        protected override void Because()
        {
            _registerView = Sut.View;
            _viewModel = Sut.View.Model;
        }        

        [Test]
        public void should_have_proper_view_model()
        {
            _viewModel.ShouldNotBeNull();
        }

        [Test]
        public void view_model_should_have_command_handler()
        {
            _viewModel.RegisterCommand.ShouldNotBeNull();
        }

        [Test]
        public void should_have_commmand_handler_executable()
        {
            _viewModel.RegisterCommand.CanExecute(null).ShouldBeTrue();
        }
    }
    
}
