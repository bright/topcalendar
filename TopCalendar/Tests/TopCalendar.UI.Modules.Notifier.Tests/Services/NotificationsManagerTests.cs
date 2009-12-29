using System;
using System.Threading;
using NUnit.Framework;
using Rhino.Mocks;
using TopCalendar.UI.Modules.Notifier.Services;
using TopCalendar.Utility.Tests;

namespace TopCalendar.UI.Modules.Notifier.Tests.Services
{
	public class when_adding_notification_to_manager : observations_for_auto_created_sut_of_type<NotificationsManager>
	{
		private INotification _notification;

		protected override void Because()
		{
			Sut.Add(_notification);
		}

		protected override void EstablishContext()
		{
			_notification = Dependency<INotification>();
			_notification.Stub(n => n.NotifyAt).Return(DateTime.Now.AddMilliseconds(100));
		}

		[Test]
		public void should_send_send_notification_before_notification_date()
		{
			Thread.Sleep((int)NotificationsManager.CheckForPendingNotificationsInterval/2);
			_notification.AssertWasNotCalled(n=> n.Notify());
		}

		[Test]
		public void should_send_notification_after_notification_date()
		{
			Thread.Sleep((int)NotificationsManager.CheckForPendingNotificationsInterval + 100);
			_notification.AssertWasCalled(n=> n.Notify());
		}

		[Test]
		public void should_send_notification_only_once()
		{
			Thread.Sleep(2*(int)NotificationsManager.CheckForPendingNotificationsInterval + 100);
			_notification.AssertWasCalled(n => n.Notify(), o=> o.Repeat.Once());
		}
	}
}