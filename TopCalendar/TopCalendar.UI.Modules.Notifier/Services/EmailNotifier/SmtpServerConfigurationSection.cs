using System;
using System.Configuration;
using System.Net;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

namespace TopCalendar.UI.Modules.Notifier.Services.EmailNotifier
{
	public class SmtpServerConfigurationSection : SerializableConfigurationSection, ISmtpServerConfiguration
	{

		public const string SectionName  = "SmtpServerConfiguration";

		[ConfigurationProperty("Host", IsRequired = true)]
		public string Host
		{
			get { return (string)base["Host"]; }			
		}

		[ConfigurationProperty("Port", IsRequired = true)]
		public int Port
		{
			get { return Convert.ToInt32(base["Port"]); }			
		}

		[ConfigurationProperty("EnableSsl", IsRequired = true)]
		public bool EnableSsl
		{
			get { return Convert.ToBoolean(base["EnableSsl"]); }			
		}

		public NetworkCredential Credentials
		{
			get
			{
				return new NetworkCredential(UserName, Password);
			}
		}

		[ConfigurationProperty("Password")]
		public string Password
		{
			get { return (string)base["Password"]; }			
		}

		[ConfigurationProperty("UserName")]
		protected string UserName
		{
			get { return (string) base["UserName"]; }			
		}
	}
}