#region

using System.Windows.Input;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Logging;
using Microsoft.Practices.Composite.Presentation.Commands;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using Ninject;
using TopCalendar.Client.Connector;
using TopCalendar.UI.Infrastructure;
using TopCalendar.Utility.UI;

#endregion

namespace TopCalendar.UI.Modules.Registration
{
    public class RegistrationPresentationModel
        : PresentationModelFor<IRegistrationView>, IRegistrationPresentationModel
    {
        private readonly IEventAggregator _eventAggregator;
        private DelegateCommand<object> _registerCommand;
        private DelegateCommand<object> _loginCommand;

        [Inject]
        public ILoggerFacade Log { get; set; }

        [Inject]
        public IUserRegistrator Registrator { get; set; }

        [Inject]
        public IUserAuthenticator Authenticator { get; set; }

        public ICommand RegisterCommand
        {
            get { return _registerCommand; }
            private set
            {
                _registerCommand = value as DelegateCommand<object>;
                OnPropertyChanged("RegisterCommand");
            }
        }

        public ICommand LoginCommand
        {
            get { return _loginCommand; }
            private set
            {
                _loginCommand = value as DelegateCommand<object>;
                OnPropertyChanged("LoginCommand");
            }
        }

        public RegistrationPresentationModel(IRegistrationView view, IEventAggregator eventAggregator)
            : base(view)
        {
            _eventAggregator = eventAggregator;
            _registerCommand = new DelegateCommand<object>(Register, CanRegister);
            _loginCommand = new DelegateCommand<object>(LoginAction);
            _cancelCommand = new DelegateCommand<object>(Cancel);
            _view.ViewModel = this;
        }

        private void Cancel(object obj)
        {
            _eventAggregator.GetEvent<UnloadViewEvent>().Publish(View);
            Log.Log("Rejestracja anulowana", Category.Info, Priority.None);
        }

        private DelegateCommand<object> _cancelCommand;

        public ICommand CancelCommand
        {
            get { return _cancelCommand; }
            set
            {
                _cancelCommand = (DelegateCommand<object>) value;
                OnPropertyChanged("CancelCommand");
            }
        }

        private string _login;

        [StringLengthValidator(4, 30)]
        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChanged("Login");
                _registerCommand.RaiseCanExecuteChanged();
            }
        }

        private string _password;

        [StringLengthValidator(4, 30)]
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged("Password");
                _registerCommand.RaiseCanExecuteChanged();
            }
        }

        private bool _loginMode = true;

        public bool LoginMode
        {
            get { return _loginMode; }
            set
            {
                _loginMode = value;
                _registerMode = !value;
                OnPropertyChanged("LoginMode");
                OnPropertyChanged("RegisterMode");
                _registerCommand.RaiseCanExecuteChanged();
            }
        }

        private bool _registerMode = false;

        public bool RegisterMode
        {
            get { return _registerMode; }
            set
            {
                _registerMode = value;
                _loginMode = !value;
                OnPropertyChanged("RegisterMode");
                OnPropertyChanged("LoginMode");
                _registerCommand.RaiseCanExecuteChanged();
            }
        }

        private bool CanRegister(object arg)
        {
            return Validation.Validate(this).IsValid;
        }

        private void Register(object obj)
        {
            bool registeredSuccessfully = Registrator.Register(Login, Password);

            if (registeredSuccessfully)
            {
                Log.Log(string.Format("{0}, {1} - zarejestrowany", Login, Password), Category.Info, Priority.None);

                _eventAggregator.GetEvent<UnloadViewEvent>().Publish(View);
                var e = _eventAggregator.GetEvent<RegistrationCompletedEvent>();
                e.Publish(Login);
            }
        }

        private void LoginAction(object obj)
        {
            bool loggedSuccessfully = Authenticator.Login(Login, Password);

            if (loggedSuccessfully)
            {
                Log.Log(string.Format("{0}, {1} - zarejestrowany", Login, Password), Category.Info, Priority.None);

                _eventAggregator.GetEvent<UnloadViewEvent>().Publish(View);
                var e = _eventAggregator.GetEvent<RegistrationCompletedEvent>();
                e.Publish(Login);
            }
        }
    }
}