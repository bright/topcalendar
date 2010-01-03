using TopCalendar.Client.DataModel;
using TopCalendar.UI.Infrastructure.CommonCommands;

namespace TopCalendar.UI.Modules.Notifier
{
	public class ShowEmailNotifierCommand : LabeledEventPublisherCommand<ShowEmailNotiferEvent,Task>
	{
		public override string Header
		{
			get { return "Powiadomienia"; }
		}
	}
}