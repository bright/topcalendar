using System;
using Microsoft.Practices.Composite.Presentation.Commands;
using Microsoft.Practices.ServiceLocation;

namespace TopCalendar.UI.Modules.Registration
{
    public class RegistrationPresenter : IRegistrationPresenter
    {
        private readonly IRegistrationView _registrationView;

        public RegistrationPresenter(IRegistrationView registrationView, IServiceLocator serviceLocator)
        {
            _registrationView = registrationView;
            _registrationView.Model = InitViewModel();
        }

        public void Register(LoginPasswordPresentationModel loginPasswordPresentationModel)
        {
            throw new Exception();
        }

        private RegistrationPresentationModel InitViewModel()
        {
            return new RegistrationPresentationModel
            {
                Login = "",
                Password = "",
                RegisterCommand = new DelegateCommand<LoginPasswordPresentationModel>(
                    Register
                )
            };
        }

        public IRegistrationView View
        {
            get { return _registrationView; }
        }
    }
}
