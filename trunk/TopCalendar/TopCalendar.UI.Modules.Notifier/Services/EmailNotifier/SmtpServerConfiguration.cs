using System.Net;
using System.Net.Mail;

namespace TopCalendar.UI.Modules.Notifier.Services.EmailNotifier
{
	public class SmtpServerConfiguration : ISmtpServerConfiguration
	{
		public string Host	 { get;  set; }
		public int Port { get;  set; }
		public bool EnableSsl { get;  set;}		
		public NetworkCredential Credentials {get;  set;}

		public static ISmtpServerConfigurator Configure() {
			return new SmtpServerConfigurator(new SmtpServerConfiguration()); 
		}
	}
}