using System;
using AutoMapper;
using NUnit.Framework;
using TopCalendar.UI.Modules.Notifier.Services;
using TopCalendar.UI.Modules.Notifier.Services.EmailNotifier;
using TopCalendar.Utility;
using TopCalendar.Utility.Tests;

namespace TopCalendar.UI.Modules.Notifier.Tests.Services.EmailNotifier
{
	public class when_building_configuration : observations_for_sut_of_type<SmtpServerConfigurator>
	{
		private TestConfiguration _test;
		private ISmtpServerConfiguration _result;

		protected override void Because()
		{
			_result = Sut.Host(_test.Host)
				.Port(_test.Port)
				.Credentials(_test.userName, _test.password)
				.UseSsl()				
				.Build();
		}

		protected override SmtpServerConfigurator CreateSut()
		{
			return new SmtpServerConfigurator();
		}

		protected override void EstablishContext()
		{
			TasksRunner.Get().Execute<MappingConfiguration>();
			_test = new TestConfiguration();
		}

		protected override void AfterEachObservation()
		{
			Mapper.Reset();
		}

		[Test]
		public void should_configure_enable_ssl_properly()
		{
			_result.EnableSsl.ShouldBeTrue();
		}

		//[Test]
		//public void should_configure_use_default_credenials()
		//{
		//    _result.UseDefaultCredentials.ShouldBeTrue();
		//}

		[Test]
		public void should_configure_host_properly()
		{
			_result.Host.ShouldEqual(_test.Host);
		}

		[Test]
		public void should_configure_port_correctly()
		{
			_result.Port.ShouldEqual(_test.Port);
		}

		[Test]
		public void should_return_different_instance_on_each_build()
		{
			Sut.Build().ShouldNotEqual(_result);
		}
	}
}