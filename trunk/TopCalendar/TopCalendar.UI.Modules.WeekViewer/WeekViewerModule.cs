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
		private readonly IPluginLoader _pluginLoader;
		private CommandCanExecuteHelper _canExecuteShowWeekView;

		public WeekViewerModule(IKernel kernel, IEventAggregator eventAggregator, ILoggerFacade loggerFacade, IMenuManager menuManager, IPluginLoader pluginLoader )
		{
			_kernel = kernel;
			_eventAggregator = eventAggregator;
			_loggerFacade = loggerFacade;
			_menuManager = menuManager;
			_pluginLoader = pluginLoader;
		}

		public void Initialize()
		{
			RegisterViewsAndServices();
			SubscribeToDefaultEvents();
			AddItemsToMenus();
		}

		private void AddItemsToMenus()
		{
			_canExecuteShowWeekView = new CommandCanExecuteHelper(false);
			_menuManager.AddItemToMenu<ShowWeekViewEvent, DateTime?>("TasksMenu", "WeekView", "Widok tygodnia", _canExecuteShowWeekView);
		}

		private void SubscribeToDefaultEvents()
		{
			_eventAggregator.GetEvent<ShowWeekViewEvent>().Subscribe(HandleShowWeekViewEvent);			
			_eventAggregator.GetEvent<RegistrationCompletedEvent>().Subscribe(HandleRegistrationCompletedEvent);
		}

		private void HandleRegistrationCompletedEvent(string obj)
		{
			_canExecuteShowWeekView.CanExecute = true;
			_pluginLoader.RegisterViewWithRegion(RegionNames.MainContent, () => _kernel.Get<IPresentationModelFor<IWeekView>>().View);
		}

		private void HandleShowWeekViewEvent(DateTime? obj)
		{
			_kernel.Get<IPluginLoader>().ActivateView(RegionNames.MainContent, ()=> _kernel.Get<IWeekView>());	
		}

		private void RegisterViewsAndServices()
		{
			_kernel.Bind<IWeekView>().To<WeekView>().InSingletonScope();
			_kernel.Bind<IPresentationModelFor<IWeekView>>().To<WeekViewPresentationModel>();
			_kernel.Bind<IWeekTaskLoader>().To<WeekTaskLoader>();
		}
	}
}
