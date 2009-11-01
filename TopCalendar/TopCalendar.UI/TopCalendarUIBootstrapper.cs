using System.Windows;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.ServiceLocation;
using Ninject;
using NinjectContrib.CompositePresentation;
using TopCalendar.Client.Connector;
using TopCalendar.UI.MenuInfrastructure;
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
			ServiceLocator.SetLocatorProvider(() => Kernel.Get<IServiceLocator>());

            // Todo: moduly docelowo powinny ladowane przy uzyciu managera pluginow :)
			Kernel.LoadModulesFromAssembly(typeof(IUserRegistrator).Assembly);

			Kernel.Bind<IShellView>().To<Shell>();

		    Kernel.Bind<IMenuManager>().To<MenuManager>();
		    Kernel.Bind<IMenuProvider>().To<MenuProvider>();
		}

		protected override DependencyObject CreateShell()
		{
		    var view = Kernel.Get<IShellView>();
		    view.Model = Kernel.Get<ShellPresentationModel>();
			view.Show();
			return view as DependencyObject;
		}
	}
}