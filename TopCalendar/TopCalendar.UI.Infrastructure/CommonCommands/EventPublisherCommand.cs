using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Presentation.Commands;
using Microsoft.Practices.Composite.Presentation.Events;
using Microsoft.Practices.ServiceLocation;

namespace TopCalendar.UI.Infrastructure.CommonCommands
{
	public abstract class EventPublisherCommand<TEvent,TArgument>
		: DelegateCommand<TArgument>		
		where TEvent : CompositePresentationEvent<TArgument>		
	{
		private static IEventAggregator _eventAggregator;

		protected EventPublisherCommand():base(PublishEvent,CanPublish)
		{			
		}

		private static bool CanPublish(TArgument arg)
		{
			return true;
		}

		private static void PublishEvent(TArgument argument)
		{
			EventAggregatorInstance.GetEvent<TEvent>().Publish(argument);
		}

		private static IEventAggregator EventAggregatorInstance
		{
			get 
			{
				if (_eventAggregator == null)
					_eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
				return _eventAggregator;
			}
		}
	}
}