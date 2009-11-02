using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Regions;
using Ninject;
using TopCalendar.UI.Infrastructure;

namespace TopCalendar.UI.Modules.Registration
{
    public class RegistrationModule : IModule
    {
		private readonly IKernel _kernel;
		private readonly IRegionManager _regionManager;

        public RegistrationModule(IKernel kernel, IRegionManager regionManager)
		{
			_kernel = kernel;
			_regionManager = regionManager;
		}

		public void Initialize()
		{
			RegisterViewsAndServices();			
			_regionManager.RegisterViewWithRegion(RegionNames.MainContent, 
				() => _kernel.Get<IRegistrationPresentationModel>().View);
		}

		private void RegisterViewsAndServices()
		{			
			_kernel.Bind<IRegistrationView>().To<RegistrationView>();
			_kernel.Bind<IRegistrationPresentationModel>().To<RegistrationPresentationModel>();
		}
    }

  
}
