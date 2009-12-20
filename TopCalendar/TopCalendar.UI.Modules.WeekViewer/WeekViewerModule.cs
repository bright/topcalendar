using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Logging;
using Microsoft.Practices.Composite.Modularity;
using Ninject;
using TopCalendar.UI.Infrastructure;
using TopCalendar.UI.MenuInfrastructure;
using TopCalendar.UI.PluginManager;
using TopCalendar.Utility.UI;

namespace TopCalendar.UI.Modules.WeekViewer
{
	public class WeekViewerModule : IModule
	{
		private readonly IKernel _kernel;
		private readonly IEventAggregator _eventAggregator;
		private readonly ILoggerFacade _loggerFacade;
		private readonly IMenuManager _menuManager;

		public WeekViewerModule(IKernel kernel, IEventAggregator eventAggregator, ILoggerFacade loggerFacade, IMenuManager menuManager )
		{
			_kernel = kernel;
			_eventAggregator = eventAggregator;
			_loggerFacade = loggerFacade;
			_menuManager = menuManager;
		}

		public void Initialize()
		{
			RegisterViewsAndServices();
			SubscribeToDefaultEvents();
			AddItemsToMenus();
		}

		private void AddItemsToMenus()
		{
			_menuManager.AddItemToMenu<ShowWeekViewEvent, DateTime?>("TasksMenu", "WeekView", "Widok tygodnia");
		}

		private void SubscribeToDefaultEvents()
		{
			_eventAggregator.GetEvent<ShowWeekViewEvent>().Subscribe(HandleShowWeekViewEvent);
		}

		private void HandleShowWeekViewEvent(DateTime? obj)
		{
			_kernel.Get<IPluginLoader>().RegisterViewWithRegion(RegionNames.MainContent, ()=> _kernel.Get<IPresentationModelFor<IWeekView>>().View);
		}

		private void RegisterViewsAndServices()
		{
			_kernel.Bind<IWeekView>().To<WeekView>();
			_kernel.Bind<IPresentationModelFor<IWeekView>>().To<WeekViewPresentationModel>();
			_kernel.Bind<IWeekTaskLoader>().To<WeekTaskLoader>();
		}
	}
}
