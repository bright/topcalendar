using System.ComponentModel;
using System.Windows.Input;

namespace TopCalendar.UI.Modules.Registration
{
    public class RegistrationPresentationModel : LoginPasswordPresentationModel, INotifyPropertyChanged
    {
        private ICommand _registerCommand;

        public ICommand RegisterCommand
        {
            get { return _registerCommand; }
            set
            {
                _registerCommand = value;
                OnPropertyChanged("RegisterCommand");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}