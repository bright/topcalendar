using System;
using System.Windows.Controls;

namespace TopCalendar.UI.Modules.Plugins
{
	/// <summary>
	/// Interaction logic for PluginsView.xaml
	/// </summary>
	public partial class PluginsView : IPluginsView
	{
		public PluginsView()
		{
			InitializeComponent();
		}

		public PluginsViewPresentationModel ViewModel
		{ 
			get { return DataContext as PluginsViewPresentationModel;}
			set { DataContext = value; }
		}
	}
}
