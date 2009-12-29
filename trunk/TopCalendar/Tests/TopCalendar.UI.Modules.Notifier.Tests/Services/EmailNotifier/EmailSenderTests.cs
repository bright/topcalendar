using System;
using NUnit.Framework;
using TopCalendar.UI.Modules.Notifier.Services.EmailNotifier;
using TopCalendar.Utility.Tests;
using TopCalendar.Utility.BasicExtensions;

namespace TopCalendar.UI.Modules.Notifier.Tests.Services.EmailNotifier
{
	[Ignore]
	public class when_sending_email : observations_for_sut_of_type<EmailSender>
	{
		private string _recipents;
		private string _subject;
		private string _body;
		private TestConfiguration _configuration;
		private Exception _exception;
		private string _from;

		protected override void Because()
		{
			_exception = ((Action) (() => Sut.Send(_from,_recipents, _subject, _body))).ThrownException();
		}

		protected override void EstablishContext()
		{
			_configuration = new TestConfiguration();
			_recipents = _configuration.userName;
			_subject = "TestEmailSubject";
			_body = "TestEmailBody";
			_from = _configuration.userName;
		}

		protected override EmailSender CreateSut()
		{			
			return new EmailSender(_configuration);
		}

		[Test]
		public void should_throw_no_exceptions()
		{
			_exception.ShouldBeNull();	
		}
	}
}