using System;
using AutoMapper;
using NUnit.Framework;
using TopCalendar.UI.Modules.Notifier.Services;
using TopCalendar.UI.Modules.Notifier.Services.EmailNotifier;
using TopCalendar.Utility;
using TopCalendar.Utility.Tests;

namespace TopCalendar.UI.Modules.Notifier.Tests.Services.EmailNotifier
{
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