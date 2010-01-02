using System;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Logging;
using Microsoft.Practices.Composite.Modularity;
using Ninject;
using TopCalendar.UI.Infrastructure;
using TopCalendar.UI.MenuInfrastructure;
using TopCalendar.UI.Modules.MonthViewer.Services;
using TopCalendar.UI.PluginManager;
using TopCalendar.Utility;
using TopCalendar.Utility.UI;

namespace TopCalendar.UI.Modules.MonthViewer
{
	public class MonthViewerModule : IModule
	{
		private readonly IKernel _kernel;		
		private readonly IEventAggregator _eventAggregator;
		private readonly ILoggerFacade _loggerFacade;
		private readonly IMenuManager _menuManager;
		private CommandCanExecuteHelper _canExecuteShowWeekView;

		public MonthViewerModule(IKernel kernel, IEventAggregator eventAggregator, ILoggerFacade loggerFacade, IMenuManager menuManager)
		{
			_kernel = kernel;			
			_eventAggregator = eventAggregator;
			_loggerFacade = loggerFacade;
			_menuManager = menuManager;
		}

		public void Initialize()
		{
			ExecuteBootsrapTasks();
			RegisterViewsAndServices();
			AddItemsToMenus();
			SubscribeToDefaultEvents();			
		}

		private void AddItemsToMenus()
		{
			_canExecuteShowWeekView = new CommandCanExecuteHelper(false);
			_menuManager.AddItemToMenu<ShowMonthViewEvent, DateTime?>("TasksMenu", "MonthView", "Widok miesiąca", _canExecuteShowWeekView);
		}

		private void SubscribeToDefaultEvents()
		{
			_eventAggregator.GetEvent<RegistrationCompletedEvent>().Subscribe(login => LoadAndShowMonthView());
			_eventAggregator.GetEvent<RegistrationCompletedEvent>().Subscribe(l => _canExecuteShowWeekView.CanExecute = true);
			_eventAggregator.GetEvent<ShowMonthViewEvent>().Subscribe(dt => ShowMonthView());
		}

		private void LoadAndShowMonthView()
		{
			_kernel.Get<IPluginLoader>().RegisterViewWithRegion(RegionNames.MainContent, () => _kernel.Get<IPresentationModelFor<IMonthView>>().View);
			ShowMonthView();
		}

		private void ShowMonthView()
		{
			_kernel.Get<IPluginLoader>().ActivateView(RegionNames.MainContent,()=> _kernel.Get<IMonthView>());
		}

		private void ExecuteBootsrapTasks()
		{
			_loggerFacade.Log("MonthViewerModule exectues bootrapper tasks", Category.Debug, Priority.None);
			
		}

		private void RegisterViewsAndServices()
		{			
			_kernel.Bind<IMonthView>().To<MonthView>().InSingletonScope();
			_kernel.Bind<IPresentationModelFor<IMonthView>>().To<MonthViewPresentationModel>().InSingletonScope();
			_kernel.Bind<IMonthTaskLoader>().To<MonthTaskLoader>().InSingletonScope();			
		}
	}
}
