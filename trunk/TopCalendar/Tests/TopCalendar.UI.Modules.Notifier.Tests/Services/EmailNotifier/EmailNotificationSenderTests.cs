using System;
using System.Net.Mail;
using NUnit.Framework;
using TopCalendar.UI.Modules.Notifier.Services.EmailNotifier;
using TopCalendar.Utility.Tests;
using Rhino.Mocks;

namespace TopCalendar.UI.Modules.Notifier.Tests.Services.EmailNotifier
{
	public class when_email_notification_sender_notifes : observations_for_auto_created_sut_of_type<EmailNotificationSender>
	{
		private IEmailNotification _notification;

		protected override void Because()
		{
			Sut.Notify(_notification);
		}

		protected override void EstablishContext()
		{
			_notification = Dependency<IEmailNotification>();
			_notification.Stub(n => n.From).Return(new MailAddress("a@a.a"));
			_notification.Stub(n => n.Recipient).Return(new MailAddress("a@a.a"));
			_notification.Stub(n => n.Subject).Return(Guid.NewGuid().ToString());
			_notification.Stub(n => n.Body).Return(Guid.NewGuid().ToString());
		}

		[Test]
		public void should_send_email_with_proper_information()
		{
			Dependency<IEmailSender>()
				.AssertWasCalled(sender=> 
					sender.Send(
						Arg.Is(_notification.From),
						Arg.Is(_notification.Recipient),
						Arg.Is(_notification.Subject),
						Arg.Is(_notification.Body)					
					));
		}
	}
}