using System.Windows;
using CommonServiceLocator.NinjectAdapter;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.ServiceLocation;
using Ninject;
using NinjectContrib.CompositePresentation;
using TopCalendar.Client.Connector;
using TopCalendar.UI.Modules.MonthViewer;
using TopCalendar.UI.Modules.Registration;

namespace TopCalendar.UI
{
	public class TopCalendarUIBootstrapper : NinjectBootstrapper
	{		
		protected override IModuleCatalog GetModuleCatalog()
		{
			var cat = new ModuleCatalog();
		    cat.AddModule(typeof (RegistrationModule));
			cat.AddModule(typeof (MonthViewerModule));
			return cat;
		}

		protected override void ConfigureKernel()
		{
			base.ConfigureKernel();
			// Todo: nie wiem czy to dobre miejsce na konfiguracje ServiceLocatora
			ServiceLocator.SetLocatorProvider(()=>Kernel.Get<IServiceLocator>());
			Kernel.LoadModulesFromAssembly(typeof(IUserRegistrator).Assembly);
			Kernel.Bind<IShellView>().To<Shell>();						
		}

		protected override DependencyObject CreateShell()
		{
			ShellPresenter presenter = Kernel.Get<ShellPresenter>();
			IShellView view = presenter.View;
			view.ShowView();
			return view as DependencyObject;
		}
	}
}