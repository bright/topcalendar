using System;
using System.Windows.Input;
using TopCalendar.Client.DataModel;

namespace TopCalendar.UI.Infrastructure.CommonCommands
{
	public class DeleteTaskCommand : EventPublisherCommand<DeleteTaskEvent,Task>
	{		
	}

	public class ShowAddNewTaskCommand : EventPublisherCommand<ShowAddNewTaskViewEvent,DateTime?>
	{
	}	
}