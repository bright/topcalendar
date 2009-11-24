﻿using System;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Logging;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.ServiceLocation;
using Ninject;
using TopCalendar.UI.Infrastructure;
using TopCalendar.UI.Modules.MonthViewer.Configuration;
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

		public MonthViewerModule(IKernel kernel, IEventAggregator eventAggregator, ILoggerFacade loggerFacade)
		{
			_kernel = kernel;			
			_eventAggregator = eventAggregator;
			_loggerFacade = loggerFacade;
		}

		public void Initialize()
		{
			ExecuteBootsrapTasks();
			RegisterViewsAndServices();
			SubscribeToDefaultEvents();
			//_regionManager.RegisterViewWithRegion(RegionNames.MainContent, 
			//    () => _kernel.Get<IMonthViewPresentationModel>().View);
		}

		private void SubscribeToDefaultEvents()
		{
			_eventAggregator.GetEvent<RegistrationCompletedEvent>()
				.Subscribe(login => LoadMonthView());
		}

		private void LoadMonthView()
		{
			_kernel.Get<IPluginLoader>()
				.RegisterViewWithRegion(RegionNames.MainContent,_kernel.Get<IPresentationModelFor<IMonthView>>().View );
		}

		private void ExecuteBootsrapTasks()
		{
			_loggerFacade.Log("MonthViewerModule exectues bootrapper tasks", Category.Debug,Priority.None);
			TasksRunner.Get().Execute<TaskToMonthViewTaskMapping>();
		}

		private void RegisterViewsAndServices()
		{			
			_kernel.Bind<IMonthView>().To<MonthView>();
			_kernel.Bind<IPresentationModelFor<IMonthView>>().To<MonthViewPresentationModel>();
			_kernel.Bind<IMonthTaskLoader>().To<MonthTaskLoader>();
		}
	}
}