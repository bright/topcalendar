using System;
using System.Configuration;
using AutoMapper;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Modularity;
using Ninject;
using TopCalendar.Client.DataModel;
using TopCalendar.UI.Infrastructure;
using TopCalendar.UI.MenuInfrastructure;
using TopCalendar.UI.Modules.Notifier.Services;
using TopCalendar.UI.Modules.Notifier.Services.EmailNotifier;
using TopCalendar.UI.PluginManager;
using TopCalendar.Utility;
using TopCalendar.Utility.UI;

namespace TopCalendar.UI.Modules.Notifier
{
	public class NotifierModule : IModule
	{
		private readonly IKernel _kernel;
		private readonly IMenuManager _menuManager;
		private readonly IPluginLoader _pluginLoader;
		private readonly IEventAggregator _eventAggregator;

		public NotifierModule(IKernel kernel, IMenuManager menuManager, IPluginLoader pluginLoader, IEventAggregator eventAggregator)
		{
			_kernel = kernel;
			_menuManager = menuManager;
			_pluginLoader = pluginLoader;
			_eventAggregator = eventAggregator;
		}

		public void Initialize()
		{
			LoadConfiguration();
			RegisterViewsAndServices();
			SubscribeToEvents();
			AddItemsToMenus();
		}

		private void SubscribeToEvents()
		{
			_eventAggregator.GetEvent<ShowEmailNotiferEvent>().Subscribe(ShowEmailNotifierView);
		}
		// takie metody powinny byc chyba w kontrolerze...
		private void ShowEmailNotifierView(Task task)
		{
			var model = _kernel.Get<IEmailNotificationViewModel>();
			model.ForTask(task);
			_pluginLoader.ActivateView(RegionNames.ModalPopupContent, model.View);
		}

		private void RegisterViewsAndServices()
		{
			_kernel.Load<ServicesNinjectModule>();
			_kernel.Bind<IEmailNotificationViewModel>().To<EmailNotificationViewModel>().InSingletonScope();
			_kernel.Bind<IEmailNotificationView>().To<EmailNotificationView>().InSingletonScope();
			_pluginLoader.RegisterInActiveViewWithRegion(RegionNames.ModalPopupContent,()=> _kernel.Get<IEmailNotificationViewModel>().View);
		}

		private void AddItemsToMenus()
		{			
			_menuManager.AddLabeledCommand<ShowEmailNotifierCommand,ShowEmailNotiferEvent,Task>();
		}

		private void LoadConfiguration()
		{
			TasksRunner.Get().Execute<MappingConfiguration>();
			var section = (SmtpServerConfigurationSection)ConfigurationManager.GetSection(SmtpServerConfigurationSection.SectionName);
			var config = Mapper.Map<SmtpServerConfigurationSection, SmtpServerConfiguration>(section);
			_kernel.Bind<ISmtpServerConfiguration>().ToConstant(config);
		}
	}
}