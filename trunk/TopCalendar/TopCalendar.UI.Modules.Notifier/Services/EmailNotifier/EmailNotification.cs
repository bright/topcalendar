using System;
using System.Net.Mail;
using TopCalendar.Client.DataModel;
using TopCalendar.Utility;
using TopCalendar.Utility.BasicExtensions;

namespace TopCalendar.UI.Modules.Notifier.Services.EmailNotifier
{
	public class EmailNotification : Notification, IEmailNotification
	{		
		private readonly DateTime _notificationDate;
		private readonly IEmailNotificationSender _sender;

		public EmailNotification(Task task, DateTime notificationDate, IEmailNotificationSender sender):base(task, task.Name)
		{			
//			Check.Guard(task.StartAt.CompareTo(notificationDate) > 0, "Notification date {0} should be smaller than task start date {1}".ToFormat(notificationDate, task.StartAt));						
			_notificationDate = notificationDate;
			_sender = sender;
		}


		public override DateTime NotifyAt
		{
			get { return _notificationDate; }
		}

		public override void Notify()
		{
			_sender.Notify(this);
		}

		public IEmailNotificationSender Sender
		{
			get { return _sender; }
		}

		public string Subject
		{
			get { return "Topcalendar - Przypomnienie - {0}".ToFormat(ForTask.Name); }
		}

		public string Body
		{
			get { return ForTask.Description; }
		}

		public MailAddress From
		{
			get; set;
		}

		public MailAddress Recipient
		{
			get; set;
		}
	}
}