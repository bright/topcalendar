using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using TopCalendar.Utility.BasicExtensions;

namespace TopCalendar.UI.Modules.Notifier.Services
{
	public interface INotificationsManager
	{
		void Add(INotification notification);
		void Remove(INotification notification);
	}

	public class NotificationsManager : INotificationsManager
	{
		private readonly IList<INotification> _notifications = new List<INotification>();
		private readonly Timer _timer;
		public const double CheckForPendingNotificationsInterval = 1000;

		public NotificationsManager()
		{
			_timer = new Timer {Interval = CheckForPendingNotificationsInterval};
			_timer.Elapsed += (o,e) => SendPendingNotifications();
			_timer.Start();
		}

		private void SendPendingNotifications()
		{
			var now = DateTime.Now;
			_notifications.Where(n => n.NotifyAt.CompareTo(now) <= 0)
				.Each(Notify);
		}

		private void Notify(INotification notification)
		{
			_notifications.Remove(notification);
			notification.Notify();
		}

		public void Add(INotification notification)
		{
			_notifications.Add(notification);
		}

		public void Remove(INotification notification)
		{			
			_notifications.Remove(notification);
		}
	}
}