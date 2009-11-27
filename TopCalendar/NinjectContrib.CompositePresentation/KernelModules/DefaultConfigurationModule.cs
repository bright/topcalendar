using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Presentation.Regions;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.ServiceLocation;
using CommonServiceLocator.NinjectAdapter;	
using Ninject.Modules;

namespace NinjectContrib.CompositePresentation.KernelModules
{	

	/// <summary>
	/// This module binds all the default Composite WPF services
	/// </summary>
	public class DefaultConfigurationModule : NinjectModule
	{
		/// <summary>
		/// Loads the module into the kernel.
		/// </summary>
		public override void Load()
		{
			Bind<IServiceLocator>().To<NinjectServiceLocator>().InSingletonScope();
			Bind<IModuleInitializer>().To<ModuleInitializer>().InSingletonScope();
			Bind<IModuleManager>().To<ModuleManager>().InSingletonScope();
			Bind<RegionAdapterMappings>().ToSelf().InSingletonScope();
			Bind<IRegionManager>().To<RegionManager>().InSingletonScope();
			Bind<IEventAggregator>().To<EventAggregator>().InSingletonScope();
			Bind<IRegionViewRegistry>().To<RegionViewRegistry>().InSingletonScope();
			Bind<IRegionBehaviorFactory>().To<RegionBehaviorFactory>().InSingletonScope();
		}
	}
}