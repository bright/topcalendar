using System.Net;
using System.Net.Mail;
using System.Runtime.Serialization;

namespace TopCalendar.UI.Modules.Notifier.Services.EmailNotifier
{
	[DataContract]
	public class SmtpServerConfiguration : ISmtpServerConfiguration
	{
		[DataMember]
		public string Host	 { get;  set; }
		[DataMember]
		public int Port { get;  set; }
		[DataMember]
		public bool EnableSsl { get;  set;}		
		[DataMember]
		public NetworkCredential Credentials {get;  set;}
		
		public static ISmtpServerConfigurator Configure() {
			return new SmtpServerConfigurator(new SmtpServerConfiguration()); 
		}
	}
}