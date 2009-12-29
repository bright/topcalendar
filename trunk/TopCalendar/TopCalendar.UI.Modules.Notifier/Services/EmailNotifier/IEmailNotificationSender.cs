using System;

namespace TopCalendar.UI.Modules.Notifier.Services.EmailNotifier
{
	public interface IEmailNotificationSender : INotificationSender<IEmailNotification>
	{		
	}

	// Todo: Check if this class is not redundant
	public class EmailNotificationSender : IEmailNotificationSender
	{
		private readonly IEmailSender _emailSender;

		public EmailNotificationSender(IEmailSender emailSender)
		{
			_emailSender = emailSender;
		}

		public void Notify(IEmailNotification notification)
		{
			_emailSender.Send(notification.From,notification.Recipient,notification.Subject,notification.Body);
		}
	}
}