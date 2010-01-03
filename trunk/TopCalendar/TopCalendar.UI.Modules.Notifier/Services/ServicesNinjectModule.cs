using System;
using Ninject.Modules;
using TopCalendar.UI.Modules.Notifier.Services.EmailNotifier;

namespace TopCalendar.UI.Modules.Notifier.Services
{
	public class ServicesNinjectModule : NinjectModule
	{
		public override void Load()
		{
			Kernel.Bind<IEmailSender>().To<EmailSender>();
			Kernel.Bind<IEmailNotificationSender>().To<EmailNotificationSender>();
			Kernel.Bind<INotificationsManager>().To<NotificationsManager>().InSingletonScope();
		}
	}
}