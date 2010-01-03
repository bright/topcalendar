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
using TopCalendar.Utility.UI;

namespace TopCalendar.UI.Modules.Notifier
{
	/// <summary>
	/// Interaction logic for EmailNotificationView.xaml
	/// </summary>
	public partial class EmailNotificationView : IEmailNotificationView
	{
		public EmailNotificationView()
		{
			InitializeComponent();
		}

		public EmailNotificationViewModel ViewModel
		{
			get { return (EmailNotificationViewModel) DataContext; }
			set { DataContext = value; }
		}
	}

	public interface IEmailNotificationView : IViewForModel<IEmailNotificationView,EmailNotificationViewModel>
	{
	}
}
