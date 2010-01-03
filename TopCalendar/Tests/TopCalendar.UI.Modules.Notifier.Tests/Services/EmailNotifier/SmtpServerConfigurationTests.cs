using System;
using System.IO;
using System.Xml.Serialization;
using AutoMapper;
using NUnit.Framework;
using TopCalendar.UI.Modules.Notifier.Services;
using TopCalendar.UI.Modules.Notifier.Services.EmailNotifier;
using TopCalendar.Utility;
using TopCalendar.Utility.Tests;

namespace TopCalendar.UI.Modules.Notifier.Tests.Services.EmailNotifier
{

	public class when_serializing_server_configuration : observations_for_sut
	{
		private SmtpServerConfiguration _config;

		protected override void Because()
		{
			var xml = new XmlSerializer(typeof (SmtpServerConfiguration));
			xml.Serialize(Console.Out,_config);
		}

		protected override void EstablishContext()
		{
			TasksRunner.Get().Execute<MappingConfiguration>();
			_config = Mapper.Map<ISmtpServerConfiguration, SmtpServerConfiguration>(new TestConfiguration());
		}

		[Test]
		public void should_()
		{
			
		}
	}


	public class when_configuring_smtp_server_with_smtp_server_configuration : observations_for_sut
	{
		private ISmtpServerConfiguration _result;

		protected override void Because()
		{
			_result = SmtpServerConfiguration.Configure().Build();
		}

		protected override void EstablishContext()
		{
			TasksRunner.Get().Execute<MappingConfiguration>();
		}

		protected override void AfterEachObservation()
		{
			Mapper.Reset();
		}

		[Test]
		public void should_return_configuration()
		{
			_result.ShouldNotBeNull();
		}
	}
}