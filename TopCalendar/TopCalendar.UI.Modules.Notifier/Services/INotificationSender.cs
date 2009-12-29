namespace TopCalendar.UI.Modules.Notifier.Services
{
	public interface INotificationSender<TNotification>
		where TNotification : INotification
	{
		void Notify(TNotification notification);
	}
}