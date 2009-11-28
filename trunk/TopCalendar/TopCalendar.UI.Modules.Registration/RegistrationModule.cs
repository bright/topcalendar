using Microsoft.Practices.Composite.Modularity;
using Ninject;
using TopCalendar.UI.Infrastructure;
using TopCalendar.UI.PluginManager;
using Microsoft.Practices.Composite.Events;
using TopCalendar.Utility.UI;

namespace TopCalendar.UI.Modules.Registration
{
    public class RegistrationModule : IModule
    {
		private readonly IKernel _kernel;
    	private readonly IPluginLoader _pluginLoader;
		private readonly IEventAggregator _eventAggregator;

        public RegistrationModule(IKernel kernel, IPluginLoader pluginLoader, IEventAggregator eventAggregator)
		{
			_kernel = kernel;
        	_pluginLoader = pluginLoader;
			_eventAggregator = eventAggregator;
		}

		public void Initialize()
		{
			RegisterViewsAndServices();

			_pluginLoader.RegisterViewWithRegion(
				RegionNames.MainContent, 
				 _kernel.Get<IRegistrationPresentationModel>().View
			);

			_eventAggregator.GetEvent<UnloadViewEvent>().Subscribe(UnloadModule);
		}

		private void UnloadModule(IView view)
		{
			if (view is IRegistrationView)
			{
				_eventAggregator.GetEvent<UnloadModuleEvent>().Publish(this);
			}
		}

		private void RegisterViewsAndServices()
		{
			_kernel.Bind<IRegistrationView>().To<RegistrationView>().InSingletonScope();
			_kernel.Bind<IRegistrationPresentationModel>().To<RegistrationPresentationModel>().InSingletonScope();
		}
    }

  
}
