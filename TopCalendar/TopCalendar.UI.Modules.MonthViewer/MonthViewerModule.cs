using System;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.ServiceLocation;
using Ninject;
using TopCalendar.UI.Infrastructure;

namespace TopCalendar.UI.Modules.MonthViewer
{
	public class MonthViewerModule : IModule
	{
		private readonly IKernel _kernel;
		private readonly IRegionManager _regionManager;
		private readonly IEventAggregator _eventAggregator;

		public MonthViewerModule(IKernel kernel, IRegionManager regionManager, IEventAggregator eventAggregator)
		{
			_kernel = kernel;
			_regionManager = regionManager;
			_eventAggregator = eventAggregator;
		}

		public void Initialize()
		{
			RegisterViewsAndServices();			
			//_regionManager.RegisterViewWithRegion(RegionNames.MainContent, 
			//    () => _kernel.Get<IMonthViewPresentationModel>().View);
		}		

		private void RegisterViewsAndServices()
		{			
			_kernel.Bind<IMonthView>().To<MonthView>();
			_kernel.Bind<IMonthViewPresentationModel>().To<MonthViewPresentationModel>();
		}
	}
}
