using System.Windows;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.ServiceLocation;
using Ninject;
using NinjectContrib.CompositePresentation;
using TopCalendar.UI.MenuInfrastructure;
using TopCalendar.UI.PluginManager;
using System;

namespace TopCalendar.UI
{
	public class TopCalendarUIBootstrapper : NinjectBootstrapper
	{
		protected override IModuleCatalog GetModuleCatalog()
		{
			return Kernel.Get<IPluginLoader>().ModuleCatalog;
		}

		protected override void ConfigureKernel()
		{
			Kernel.Bind<IPluginLoader>().To<PluginLoader>().InSingletonScope();
			Kernel.Bind<IShellView>().To<Shell>().InSingletonScope();
		    Kernel.Bind<IMenuManager>().To<MenuManager>().InSingletonScope();
		    Kernel.Bind<IMenuProvider>().To<MenuProvider>().InSingletonScope();

			base.ConfigureKernel();
			ServiceLocator.SetLocatorProvider(() => Kernel.Get<IServiceLocator>());
		}

		protected override DependencyObject CreateShell()
		{
			LoadConnectors();
			SaveWorkingDirectory();

		    var view = Kernel.Get<IShellView>();
		    view.Model = Kernel.Get<ShellPresentationModel>();
			view.Show();

			return view as DependencyObject;
		}

		protected virtual void LoadConnectors()
		{
			Kernel.Get<IPluginLoader>().LoadConnectors();
		}

		private void SaveWorkingDirectory()
		{
			if (Application.Current != null)
			{
				Application.Current.Properties["workingDir"] = Environment.CurrentDirectory;
			}
		}
	}
}