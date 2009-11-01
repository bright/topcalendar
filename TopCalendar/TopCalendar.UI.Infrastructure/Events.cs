using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Presentation.Events;

namespace TopCalendar.UI.Infrastructure
{
	/// <summary>
	/// Event publikowany po wybraniu "Zakończ" z menu
	/// W Shellu musi być subskrybent, który go obsłuży i zamknie aplikację
	/// </summary>
    public class CloseAppEvent : CompositePresentationEvent<object>
    {
    }

    public class TestEvent : CompositePresentationEvent<object>
    {
    }
}
