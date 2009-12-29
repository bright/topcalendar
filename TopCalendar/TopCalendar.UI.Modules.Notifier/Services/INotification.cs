using System;
using TopCalendar.Client.DataModel;

namespace TopCalendar.UI.Modules.Notifier.Services
{
	public interface INotification
	{
		Task ForTask { get; }
		DateTime NotifyAt { get; }
		void Notify();
	}

	public interface INotification<TNotification,TNotificationSender> : INotification where TNotificationSender : INotificationSender<TNotification>
	                                                                                  where TNotification : INotification
	{		
		TNotificationSender Sender { get; }		
	}
}