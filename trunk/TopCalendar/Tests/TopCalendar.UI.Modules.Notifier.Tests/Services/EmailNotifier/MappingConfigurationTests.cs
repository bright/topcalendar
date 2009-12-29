using System;
using AutoMapper;
using NUnit.Framework;
using TopCalendar.UI.Modules.Notifier.Services;
using TopCalendar.UI.Modules.Notifier.Services.EmailNotifier;
using TopCalendar.Utility.Tests;
using TopCalendar.Utility.BasicExtensions;

namespace TopCalendar.UI.Modules.Notifier.Tests.Services.EmailNotifier
{
	public class when_mapping_configurations : observations_for_mapping_configuration_defined_in<MappingConfiguration>
	{
		private ISmtpServerConfiguration _input;
		private SmtpServerConfiguration _result;
		private Exception _exception;

		protected override void Because()
		{
			_exception = ((Action) delegate { _result = Mapper.Map<ISmtpServerConfiguration, SmtpServerConfiguration>(_input); }).ThrownException();
		}

		protected override void AfterEachObservation()
		{
			Mapper.Reset();
			base.AfterEachObservation();
		}

		protected override void EstablishContext()
		{
			base.EstablishContext();
			_input = new TestConfiguration();			
		}

		[Test]
		public void should_throw_no_exceptions()
		{
			_exception.ShouldBeNull();
		}

		[Test]
		public void mapper_configuration_should_be_valid()
		{
			assert_mapper_configuration_is_valid();
		}
	}
}