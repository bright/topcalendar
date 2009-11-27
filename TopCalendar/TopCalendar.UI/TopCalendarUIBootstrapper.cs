using System.Windows;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.ServiceLocation;
using Ninject;
using NinjectContrib.CompositePresentation;
using TopCalendar.UI.MenuInfrastructure;
using TopCalendar.UI.PluginManager;

namespace TopCalendar.UI
{
	public class TopCalendarUIBootstrapper : NinjectBootstrapper
	{
		private readonly ModuleCatalog _moduleCatalog = new ModuleCatalog();

		protected override IModuleCatalog GetModuleCatalog()
		{
			return _moduleCatalog;
		}

		protected override void ConfigureKernel()
		{
			base.ConfigureKernel();
			ServiceLocator.SetLocatorProvider(() => Kernel.Get<IServiceLocator>());

			Kernel.Bind<IPluginLoader>().To<PluginLoader>().InSingletonScope();
			Kernel.Bind<IShellView>().To<Shell>().InSingletonScope();
		    Kernel.Bind<IMenuManager>().To<MenuManager>().InSingletonScope();
		    Kernel.Bind<IMenuProvider>().To<MenuProvider>().InSingletonScope();
		}

		protected override DependencyObject CreateShell()
		{
			LoadPlugins();

		    var view = Kernel.Get<IShellView>();
		    view.Model = Kernel.Get<ShellPresentationModel>();
			view.Show();

			return view as DependencyObject;
		}

		protected virtual void LoadPlugins()
		{
			var loader = Kernel.Get<IPluginLoader>();
			loader.Load(_moduleCatalog);
		}
	}
}