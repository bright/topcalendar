using System.Windows.Controls;

namespace TopCalendar.UI.Modules.WeekViewer
{
	/// <summary>
	/// Interaction logic for WeekView.xaml
	/// </summary>
	public partial class WeekView : UserControl, IWeekView
	{
		public WeekView()
		{
			InitializeComponent();
		}

		public WeekViewPresentationModel ViewModel
		{
			get { return (WeekViewPresentationModel) DataContext; }
			set { DataContext = value; }
		}
	}
}
