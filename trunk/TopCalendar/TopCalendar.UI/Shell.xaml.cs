using System.Windows;

namespace TopCalendar.UI
{
	/// <summary>
	/// Interaction logic for Shell.xaml
	/// </summary>
	public partial class Shell : Window, IShellView
	{
		public Shell()
		{
		    InitializeComponent();
		}

        public ShellPresentationModel Model
        {
            get
            {
                return DataContext as ShellPresentationModel;
            }
            set
            {
                DataContext = value;
            }
        }
	}
}
