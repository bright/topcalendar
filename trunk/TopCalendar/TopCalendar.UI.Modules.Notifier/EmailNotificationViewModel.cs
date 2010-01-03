using System;
using System.Net.Mail;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Presentation.Commands;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using Microsoft.Practices.ServiceLocation;
using Ninject;
using Ninject.Parameters;
using TopCalendar.Client.DataModel;
using TopCalendar.UI.Infrastructure;
using TopCalendar.UI.Infrastructure.CommonCommands;
using TopCalendar.UI.Modules.Notifier.Services;
using TopCalendar.UI.Modules.Notifier.Services.EmailNotifier;
using TopCalendar.Utility.UI;

namespace TopCalendar.UI.Modules.Notifier
{

	public class EmailNotificationViewModel : PresentationModelFor<IEmailNotificationView>, IEmailNotificationViewModel
	{
		private readonly IEventAggregator _eventAggregator;
		private readonly ISmtpServerConfiguration _smtpServerConfiguration;
		private readonly INotificationsManager _notificationsManager;

		public EmailNotificationViewModel(IEmailNotificationView view, IEventAggregator eventAggregator, ISmtpServerConfiguration smtpServerConfiguration, INotificationsManager  notificationsManager) : base(view)
		{
			_eventAggregator = eventAggregator;
			_smtpServerConfiguration = smtpServerConfiguration;
			_notificationsManager = notificationsManager;
			AddNotification = new DelegateCommand<object>(AddNotificationImpl, CanAddNotification);
			Cancel = new DelegateCommand<object>(CancelImpl);
			Sender = _smtpServerConfiguration.Credentials.UserName;
			view.ViewModel = this;			
		}

		private void CancelImpl(object obj)
		{
			_eventAggregator.GetEvent<DeactivateViewEvent>().Publish(View);
		}

		private bool CanAddNotification(object arg)
		{
			return Validation.Validate(this).IsValid;
		}

		private void AddNotificationImpl(object obj)
		{
            _notificationsManager.Add(BuildNotification());
			Cancel.Execute(null);
		}

		private INotification BuildNotification()
		{
			return new EmailNotification(_task, Date, ServiceLocator.Current.GetInstance<IEmailNotificationSender>())
			       	{
                        From = new MailAddress(Sender),
						Recipient = new MailAddress(Recipient)
			       	};
		}

		private string _sender;

		[RegexValidator(
		@"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
		+ @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
		+ @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
		+ @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$",MessageTemplate="Podaj prawidlowy email")]
		[NotNullValidator]
		public string Sender
		{
			get { return _sender; }
			set { _sender = value; 
				OnPropertyChanged(()=> Sender);
				AddNotification.RaiseCanExecuteChanged();
			}
		}

		private string _recipent;
		[RegexValidator(
		@"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
		+ @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
		+ @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
		+ @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$", MessageTemplate="Podaj prawidlowy email")]
		[NotNullValidator]
		public string Recipient
		{
			get { return _recipent; }
			set { _recipent = value; 
				OnPropertyChanged(()=> Recipient);
				AddNotification.RaiseCanExecuteChanged();
			}
		}


		private DateTime _date;
		public DateTime Date
		{
			get { return _date; }
			set { _date = value; 
				OnPropertyChanged(()=>Date);
				AddNotification.RaiseCanExecuteChanged();
			}
		}


		//private DateTime _maxDate;

		//public DateTime MaxDate
		//{
		//    get { return _maxDate; }
		//    private set { _maxDate = value;
		//        OnPropertyChanged(()=>MaxDate);
		//    }
		//}

		
		

		private DelegateCommand<object> _addNotification;
		public DelegateCommand<object> AddNotification
		{
			get { return _addNotification; }
			set { _addNotification = value; 
				OnPropertyChanged(()=> AddNotification);				
			}
		}

		private DelegateCommand<object> _cancel;
		public DelegateCommand<object> Cancel
		{
			get { return _cancel; }
			set { _cancel = value; 
				OnPropertyChanged(()=> Cancel);
			}
		}

		private Task _task;
		public void ForTask(Task task)
		{
			_task = task;
			Date = _task.StartAt;
		}
	}

	public interface IEmailNotificationViewModel : IPresentationModelFor<IEmailNotificationView>
	{
		void ForTask(Task task);
	}
}