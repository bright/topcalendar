using System;
using System.Net.Mail;
using NUnit.Framework;
using Rhino.Mocks;
using TopCalendar.Client.DataModel;
using TopCalendar.UI.Modules.Notifier.Services.EmailNotifier;
using TopCalendar.Utility.Tests;

namespace TopCalendar.UI.Modules.Notifier.Tests.Services.EmailNotifier
{

	public abstract class observations_for_email_notification : observations_for_auto_created_sut_of_type<EmailNotification>
	{
		protected Task _task;
		protected DateTime _notificationDate;
		protected MailAddress _address = new MailAddress("a@a.a");

		protected override void EstablishContext()
		{
			base.EstablishContext();
			_task = new Task(Guid.NewGuid().ToString(), DateTime.Now);
			_notificationDate = _task.StartAt.AddDays(1);
			ProvideImplementationOf(new EmailNotification(_task, _notificationDate, Dependency<IEmailNotificationSender>())
			                        	{
			                        		From = _address,
											Recipient = _address
			                        	}
				);
		}
	}

	public class when_creating_email_notification : observations_for_email_notification
	{			
		protected override void Because()
		{			
		}		

		[Test]
		public void should_have_properly_set_notification_date()
		{
			Sut.NotifyAt.ShouldEqual(_notificationDate);
		}

		[Test]
		public void should_have_proper_body()
		{
			Sut.Subject.ShouldContain(_task.Name);
		}

		[Test]
		public void should_have_proper_subject()
		{
			Sut.Body.ShouldEqual(_task.Description);
		}

		[Test]
		public void should_have_proper_from()
		{
			Sut.From.ShouldEqual(_address);
		}

		[Test]
		public void should_have_proper_recipient()
		{
			Sut.Recipient.ShouldEqual(_address);
		}

		[Test]
		public void should_have_proper_sender()
		{
			Sut.Sender.ShouldEqual(Dependency<IEmailNotificationSender>());
		}
	}

	public class when_sending_email_notification : observations_for_email_notification
	{
		protected override void Because()
		{
			Sut.Notify();
		}

		[Test]
		public void should_call_its_sender()
		{
			Dependency<IEmailNotificationSender>().AssertWasCalled(sender => sender.Notify(Arg.Is(Sut)));
		}
	}
}