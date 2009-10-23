using System.Windows.Controls;

namespace TopCalendar.UI.Modules.Registration
{
    /// <summary>
    /// Interaction logic for RegistrationView.xaml
    /// </summary>
    public partial class RegistrationView : UserControl, IRegistrationView
    {
        public RegistrationView()
        {
            InitializeComponent();
        }

        public RegistrationPresentationModel Model
        {
            get
            {
                return this.DataContext as RegistrationPresentationModel;
            }
            set
            {
                this.DataContext = value;
            }
        }
    }
}
