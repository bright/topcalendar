using System;
using NUnit.Framework;
using TopCalendar.Client.DataModel;
using TopCalendar.UI.Infrastructure;
using TopCalendar.UI.Modules.Notifier.Services;
using TopCalendar.UI.Modules.Notifier.Services.EmailNotifier;
using TopCalendar.UI.Modules.Notifier.Tests.Services.EmailNotifier;
using TopCalendar.Utility.Tests;
using TopCalendar.Utility.Tests.UI;
using Rhino.Mocks;
using TopCalendar.Utility.UI;

namespace TopCalendar.UI.Modules.Notifier.Tests.Services
{

	public abstract class observations_for_email_notification_with_stubbed_view : observations_for_presentation_model_with_stubbed_view<EmailNotificationViewModel,IEmailNotificationView>
	{
		protected Task _task;
		protected DateTime _date;
		protected ISmtpServerConfiguration _testConfiguration;


		protected override void EstablishContext()
		{
			base.EstablishContext();
			_testConfiguration = new TestConfiguration();
			ProvideImplementationOf(_testConfiguration);
			_date = DateTime.Now;
			_task = new Task(Guid.NewGuid().ToString(), _date);
		}		
	}


	public class when_loading_task_in_email_notification_view_model : observations_for_email_notification_with_stubbed_view
	{
		
		protected override void Because()
		{
			Sut.ForTask(_task);
		}

		[Test]
		public void should_set_max_date_to_task_start_date()
		{
			Sut.Date.ShouldEqual(_date);
		}
	}

	public class when_adding_notification_with_email_notification_view_model : observations_for_email_notification_with_stubbed_view
	{
		private IView _deactivatedView;

		protected override void Because()
		{
			Sut.AddNotification.Execute(null);			
		}

		protected override void EstablishContext()
		{
			base.EstablishContext();
			EventAggr.GetEvent<DeactivateViewEvent>().Subscribe(v => _deactivatedView = v);
		}

		protected override void AfterSutCreation()
		{
			base.AfterSutCreation();
			Sut.ForTask(_task);
			Sut.Sender = _testConfiguration.Credentials.UserName;
			Sut.Recipient = _testConfiguration.Credentials.UserName;
			Sut.Date = _date.AddDays(-1);
		}

		[Test]
		public void should_be_able_to_exceute_add_notification()
		{
			Sut.AddNotification.CanExecute(null).ShouldBeTrue();
		}

		[Test]
		public void should_add_notification_to_manager()
		{
			Dependency<INotificationsManager>().AssertWasCalled(man=> man.Add(
					Arg<INotification>.Matches(n=> n.NotifyAt.Equals(Sut.Date)
						&& n.ForTask.Equals(_task)
					)
				));
		}

		[Test]
		public void should_deactivate_email_notifications_view()
		{
			_deactivatedView.ShouldEqual(Sut.View);
		}
	}
}