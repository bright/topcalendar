using System;
using NUnit.Framework;
using TopCalendar.Client.DataModel;
using TopCalendar.UI.MenuInfrastructure;
using TopCalendar.UI.Modules.Notifier.Services.EmailNotifier;
using TopCalendar.UI.PluginManager;
using TopCalendar.Utility.Tests;
using Rhino.Mocks;

namespace TopCalendar.UI.Modules.Notifier.Tests
{
	public class when_initializing_notifier_module : observations_for_auto_created_sut_of_type_with_eventaggregator<NotifierModule>
	{
		protected override void Because()
		{
			Sut.Initialize();
		}

		protected override void EstablishContext()
		{
			base.EstablishContext();
			MarkNonMocked<IEmailSender>();
			MarkNonMocked<IEmailNotificationSender>();
			MarkNonMocked<IEmailNotificationView>();
			MarkNonMocked<IEmailNotificationViewModel>();
		}

		[Test]
		public void should_load_configuration()
		{
			Dependency<ISmtpServerConfiguration>().Host.ShouldNotBeEmpty();
		}

		[Test]
		public void should_bind_email_sender()
		{
			IsTypeBinded<IEmailSender>().ShouldBeTrue();
		}

		[Test]
		public void should_bind_email_notification_sender()
		{
			IsTypeBinded<IEmailNotificationSender>().ShouldBeTrue();
		}

		[Test]
		public void should_bind_email_notification_view_model()
		{
			IsTypeBinded<IEmailNotificationViewModel>().ShouldBeTrue();
		}

		[Test]
		public void should_bind_email_notification_view()
		{
			IsTypeBinded<IEmailNotificationView>().ShouldBeTrue();
		}

		[Test]
		public void should_add_item_to_menu_manager()
		{
			Dependency<IMenuManager>().AssertWasCalled(menu=> menu.AddLabeledCommand<ShowEmailNotifierCommand,ShowEmailNotiferEvent,Task>());
		}
	}

	public class when_show_email_notifier_event_occurs_after_intializing_notifier_module : observations_for_auto_created_sut_of_type_with_eventaggregator<NotifierModule>
	{
		private Task _task;
		private IEmailNotificationView _view;

		protected override void Because()
		{
			EventAggr.GetEvent<ShowEmailNotiferEvent>().Publish(_task);
		}

		protected override void AfterSutCreation()
		{
			Sut.Initialize();
		}

		protected override void EstablishContext()
		{
			base.EstablishContext();
			_task = new Task();
			_view = Stub<IEmailNotificationView>();
			Dependency<IEmailNotificationViewModel>().Stub(x => x.View).Return(_view);
		}

		[Test]
		public void should_load_task_to_view_model()
		{
			Dependency<IEmailNotificationViewModel>().AssertWasCalled(model=> model.ForTask(Arg.Is(_task)));
		}

		[Test]
		public void should_activate_email_notifier_view()
		{
			Dependency<IPluginLoader>().AssertWasCalled(loader=> loader.ActivateView(Arg<string>.Is.NotNull, Arg.Is(_view)));
		}
	}

	
}