using System.Windows;
using Microsoft.Practices.Composite.UnityExtensions;
using NinjectContrib.CompositePresentation;

namespace TopCalendar.UI
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			RunInDebugMode();

			ShutdownMode = System.Windows.ShutdownMode.OnMainWindowClose;
		}

		private void RunInDebugMode()
		{
			NinjectBootstrapper bootstrapper = new TopCalendarUIBootstrapper();
			bootstrapper.Run();
		}
	}
}
