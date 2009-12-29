using System;
using TopCalendar.Client.DataModel;
using TopCalendar.Utility;

namespace TopCalendar.UI.Modules.Notifier.Services
{
	public abstract class Notification : INotification
	{		
		public string Message { get; set; }

		protected Notification(Task forTask, string message)
		{
			Check.Guard(forTask != null, "Cannot create notification with null task");
			ForTask = forTask;
			Message = message;
		}

		public Task ForTask
		{
			get; set;
		}

		public virtual DateTime NotifyAt
		{
			get { return ForTask.StartAt; }
		}

		public abstract void Notify();
	}
}