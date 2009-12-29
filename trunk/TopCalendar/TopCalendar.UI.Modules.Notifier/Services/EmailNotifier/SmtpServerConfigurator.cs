using System;
using System.Net;
using AutoMapper;

namespace TopCalendar.UI.Modules.Notifier.Services.EmailNotifier
{
	public class SmtpServerConfigurator : ISmtpServerConfigurator
	{
		private SmtpServerConfiguration _current;

		public SmtpServerConfigurator():this(new SmtpServerConfiguration())
		{			
		}

		public SmtpServerConfigurator(SmtpServerConfiguration configuration)
		{
			_current = configuration;
		}

		public ISmtpServerConfiguration Build()
		{
			return Mapper.Map<SmtpServerConfiguration, SmtpServerConfiguration>(_current);
		}

		private SmtpServerConfiguration CopyCurrent()
		{
			return Mapper.Map<SmtpServerConfiguration, SmtpServerConfiguration>(_current);
		}

		public ISmtpServerConfigurator Host(string host)
		{
			return NewBuilderInstance(b => b._current.Host = host);
		}

		public ISmtpServerConfigurator Port(int port)
		{
			return NewBuilderInstance(b => b._current.Port = port);
		}

		public ISmtpServerConfigurator Credentials(NetworkCredential credentials)
		{
			return NewBuilderInstance(b => b._current.Credentials = credentials);
		}

		public ISmtpServerConfigurator Credentials(string userName,string password)
		{
			return Credentials(new NetworkCredential(userName, password));
		}

		public ISmtpServerConfigurator UseSsl()
		{
			return NewBuilderInstance(b => b._current.EnableSsl = true);
		}

		protected SmtpServerConfigurator NewBuilderInstance(Action<SmtpServerConfigurator> setThings)
		{
			var configurator = new SmtpServerConfigurator(CopyCurrent());
			setThings(configurator);
			return configurator;			
		}
	}
}