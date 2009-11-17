using Microsoft.Practices.Composite.Modularity;
using Ninject;
using TopCalendar.UI.Infrastructure;
using TopCalendar.UI.PluginManager;

namespace TopCalendar.UI.Modules.Registration
{
    public class RegistrationModule : IModule
    {
		private readonly IKernel _kernel;
    	private readonly IPluginLoader _pluginLoader;

        public RegistrationModule(IKernel kernel, IPluginLoader pluginLoader)
		{
			_kernel = kernel;
        	_pluginLoader = pluginLoader;
		}

		public void Initialize()
		{
			RegisterViewsAndServices();

			_pluginLoader.RegisterViewWithRegion(
				RegionNames.MainContent, 
				 _kernel.Get<IRegistrationPresentationModel>().View
			);
		}

		private void RegisterViewsAndServices()
		{			
			_kernel.Bind<IRegistrationView>().To<RegistrationView>();
			_kernel.Bind<IRegistrationPresentationModel>().To<RegistrationPresentationModel>();
		}
    }

  
}
