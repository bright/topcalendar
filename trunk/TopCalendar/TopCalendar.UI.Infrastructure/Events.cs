using Microsoft.Practices.Composite.Presentation.Events;
using TopCalendar.Utility.UI;

namespace TopCalendar.UI.Infrastructure
{
	/// <summary>
	/// Event publikowany po wybraniu "Zakończ" z menu
	/// W Shellu musi być subskrybent, który go obsłuży i zamknie aplikację.
	/// W parametrze nic :)
	/// </summary>
    public class CloseAppEvent : CompositePresentationEvent<object>
    {
    }

	/// <summary>
	/// Event publikowany przed moduly, ktore chca, zeby je ubic i wyladowac z regionu
	/// W parametrze obiekt widoku do wywalenia
	/// </summary>
	public class UnloadModuleEvent : CompositePresentationEvent<IView>
	{
	}

	/// <summary>
	/// Event publikowany po pomyslnym zakonczeniu rejestracji.
	/// W parametrze Login
	/// </summary>
    public class RegistrationCompletedEvent : CompositePresentationEvent<string>
    {
    }
}
