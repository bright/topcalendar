using System;
using System.Windows.Input;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Presentation.Commands;
using Microsoft.Practices.Composite.Presentation.Events;
using Microsoft.Practices.ServiceLocation;
using TopCalendar.Client.DataModel;

namespace TopCalendar.UI.Infrastructure.CommonCommands
{
	public class TasksCommands
	{
		public ICommand ShowTaskEditor
		{
			get { return new ShowAddNewTaskCommand(); }
		}
	}

	public class DeleteTaskCommand : DelegateCommand<Task>
	{
		public DeleteTaskCommand():base(DeleteTask, dt=>true)
		{
		}

		private static void DeleteTask(Task taskToDelete)
		{
			ServiceLocator.Current.GetInstance<IEventAggregator>()
				.GetEvent<DeleteTaskEvent>().Publish(taskToDelete);
		}
	}

	public class ShowAddNewTaskCommand : DelegateCommand<DateTime?>
	{
		public ShowAddNewTaskCommand() : base(ShowAddNewTask, dt=> true)
		{
		}

		private static void ShowAddNewTask(DateTime? dateTime)
		{
			var eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
			eventAggregator.GetEvent<ShowAddNewTaskViewEvent>().Publish(dateTime);
		}		
	}

	
}