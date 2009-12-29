using System.Net;

namespace TopCalendar.UI.Modules.Notifier.Services.EmailNotifier
{
	public interface ISmtpServerConfigurator
	{
		ISmtpServerConfiguration Build();
		ISmtpServerConfigurator Host(string host);
		ISmtpServerConfigurator Port(int port);
		ISmtpServerConfigurator Credentials(NetworkCredential credentials);
		ISmtpServerConfigurator Credentials(string userName,string password);
		ISmtpServerConfigurator UseSsl();		
	}
}