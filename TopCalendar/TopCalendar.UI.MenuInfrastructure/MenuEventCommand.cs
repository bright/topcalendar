using System;
using System.Windows.Input;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Presentation.Events;
using Microsoft.Practices.ServiceLocation;

namespace TopCalendar.UI.MenuInfrastructure
{
    public class MenuEventCommand<TEvent> : MenuEventCommand<TEvent, object>
        where TEvent : CompositePresentationEvent<object>
    {
        public MenuEventCommand(IServiceLocator serviceLocator) : base(serviceLocator)
        {
        }
    }

    public class MenuEventCommand<TEvent, TParam> : ICommand
        where TEvent : CompositePresentationEvent<TParam>
    {
        private readonly TEvent _event;

        public MenuEventCommand(IServiceLocator serviceLocator)
        {
            var eventAggregator = serviceLocator.GetInstance<IEventAggregator>();
            _event = eventAggregator.GetEvent<TEvent>();
        }

        public void Execute(object parameter)
        {
            object arg = null;
            try
            {
                arg = (TParam) parameter;
                _event.Publish((TParam)arg);
                return;
            }catch(Exception){}

            _event.Publish(default(TParam));
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
    }
}