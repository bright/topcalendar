using System.Net.Mail;

namespace TopCalendar.UI.Modules.Notifier.Services.EmailNotifier
{
	public interface IEmailNotification : INotification<IEmailNotification,IEmailNotificationSender>
	{
		string Subject { get; }
		string Body { get; }
		MailAddress From { get; }
		MailAddress Recipient { get; }
	}
}