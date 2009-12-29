using System.Net;
using System.Net.Mail;

namespace TopCalendar.UI.Modules.Notifier.Services.EmailNotifier
{
	public interface ISmtpServerConfiguration
	{
		string Host { get;}
		int Port { get; }
		bool EnableSsl { get;}		
		NetworkCredential Credentials { get; }
	}
}