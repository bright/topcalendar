using System;
using System.Windows.Controls;

namespace TopCalendar.UI.Modules.Registration
{
    /// <summary>
    /// Interaction logic for RegistrationView.xaml
    /// </summary>
    public partial class RegistrationView : IRegistrationView
    {
        public RegistrationView()
        {
            InitializeComponent();
        }

    	public RegistrationPresentationModel ViewModel
    	{
    		get { return (RegistrationPresentationModel) DataContext; }
    		set { DataContext = value; }
    	}
    }
}
