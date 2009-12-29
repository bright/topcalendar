using System.Net;
using System.Net.Mail;
using TopCalendar.UI.Modules.Notifier.Services.EmailNotifier;

namespace TopCalendar.UI.Modules.Notifier.Tests.Services.EmailNotifier
{
	public class TestConfiguration : ISmtpServerConfiguration
	{
		public readonly string userName = "";//"piotr.mionskowski@gmail.com";
		public readonly string password = "";

		public string Host
		{
			get { return "smtp.gmail.com"; }
		}

		public int Port
		{
			get { return 587; }
		}

		public bool EnableSsl
		{
			get { return true; }
		}

		public NetworkCredential Credentials
		{
			get { return new NetworkCredential(userName,password); }
		}
	}
}