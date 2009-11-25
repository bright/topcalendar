using System;
using System.Windows.Input;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Presentation.Events;
using Microsoft.Practices.ServiceLocation;

namespace TopCalendar.UI.Infrastructure.CommonCommands
{
	public class TasksCommands
	{
		public ICommand ShowTaskEditor
		{
			get { return new ShowTaskEditorCommand(); }
		}
	}

	public class ShowTaskEditorCommand : ICommand
	{
		public void Execute(object parameter)
		{
			var eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
			eventAggregator.GetEvent<ShowTaskEditorEvent>().Publish((DateTime?)parameter);
		}

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public event EventHandler CanExecuteChanged;
	}

	public class ShowTaskEditorEvent : CompositePresentationEvent<DateTime?>
	{
	}
}