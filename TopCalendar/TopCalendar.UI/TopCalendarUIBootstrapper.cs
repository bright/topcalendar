using System.Windows;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.ServiceLocation;
using Ninject;
using NinjectContrib.CompositePresentation;
using TopCalendar.UI.MenuInfrastructure;
using TopCalendar.UI.PluginManager;
using System;
using Microsoft.Practices.Composite.Regions;
using TopCalendar.UI.Infrastructure;
using TopCalendar.UI.Infrastructure.Regions;
using Microsoft.Practices.Composite.Presentation.Regions;
using Microsoft.Practices.Composite.Events;

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

		protected override void InitializeModules()
		{
			base.InitializeModules();

			var rm = Kernel.Get<IRegionManager>();
			if (rm.Regions.ContainsRegionWithName(RegionNames.MainContent))
			{
				rm.Regions[RegionNames.MainContent].Behaviors.Add(
					LastViewIsActiveRegionBehavior.BehaviorKey,
					new LastViewIsActiveRegionBehavior(Kernel.Get<IEventAggregator>())
				);
			}
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