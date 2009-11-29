using System;
using System.Windows.Input;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Presentation.Events;
using Microsoft.Practices.ServiceLocation;

namespace TopCalendar.UI.MenuInfrastructure
{
	/// <summary>
	/// Command do wpiecia do menu, publikujacy eventa z parametrem object
	/// </summary>
	/// <typeparam name="TEvent">Typ eventa</typeparam>
    public class MenuEventCommand<TEvent> : MenuEventCommand<TEvent, object>
        where TEvent : CompositePresentationEvent<object>
    {
		public MenuEventCommand(IServiceLocator serviceLocator, CommandCanExecuteHelper canExecute)
			: base(serviceLocator, canExecute)
		{
		}
    }

	/// <summary>
	/// Command do wpiecia do menu, publikujacy eventa z podanym parametrem
	/// </summary>
	/// <typeparam name="TEvent">Typ eventa</typeparam>
	/// <typeparam name="TParam">Typ parametru eventa</typeparam>
    public class MenuEventCommand<TEvent, TParam> : ICommand
        where TEvent : CompositePresentationEvent<TParam>
    {
        private readonly TEvent _event;
		private readonly CommandCanExecuteHelper _canExecute;

		public MenuEventCommand(IServiceLocator serviceLocator, CommandCanExecuteHelper canExecute)
		{
			var eventAggregator = serviceLocator.GetInstance<IEventAggregator>();
			_event = eventAggregator.GetEvent<TEvent>();

			_canExecute = canExecute;
			if (_canExecute != null)
			{
				_canExecute.CanExecuteChanged += NotifyCanExecuteChanged;
			}
		}

		private void NotifyCanExecuteChanged(object sender, EventArgs e)
		{
			if (CanExecuteChanged != null)
			{
				CanExecuteChanged(this, e);
			}
		}

        public void Execute(object parameter)
        {
            object arg = null;
            try
            {
                arg = (TParam) parameter;
                _event.Publish((TParam)arg);
                return;
            }
			catch(Exception)
			{
			}

            _event.Publish(default(TParam));
        }

        public bool CanExecute(object parameter)
        {
			if (_canExecute == null)
			{
				return true;
			}

            return _canExecute.CanExecute;
        }

        public event EventHandler CanExecuteChanged;
    }
}