using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
