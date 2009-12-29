using System;
using System.Net.Mail;
using TopCalendar.Utility;

namespace TopCalendar.UI.Modules.Notifier.Services.EmailNotifier
{
	public class EmailSender : IEmailSender
	{
		private readonly ISmtpServerConfiguration _configuration;

		private SmtpClient _client;
		private SmtpClient Client
		{
			get
			{
				if(_client == null)
					_client = InitializeSmtpClient();
				return _client;
			}
		}

		private SmtpClient InitializeSmtpClient()
		{
			return new SmtpClient(_configuration.Host,_configuration.Port) 
			{	Credentials = _configuration.Credentials,
				DeliveryMethod = SmtpDeliveryMethod.Network, 
				EnableSsl = _configuration.EnableSsl
			};
		}

		public EmailSender(ISmtpServerConfiguration configuration)
		{
			Check.Guard(configuration != null, "Configuration cannot be null");
			_configuration = configuration;
		}

		public void Send(MailAddress from, MailAddress to, string subject, string message)
		{
			using (var mailMessage = new MailMessage(from, to) { Subject = subject, Body = message })
			{
				Client.Send(mailMessage);
			}
		}

		public void Send(string from,string recipient, string subject, string body)
		{	
			//Todo: PM wywalic adresata do kernela
			Send(new MailAddress(from), new MailAddress(recipient), subject,body);
		}
	}

	public interface IEmailSender
	{
		void Send(string from,string recipient, string subject, string body);
		void Send(MailAddress from, MailAddress to, string subject, string message);
	}
}