using Microsoft.Practices.Composite.Presentation.Events;
using TopCalendar.UI.Infrastructure.CommonCommands;

namespace TopCalendar.UI.MenuInfrastructure
{
    public interface IMenuManager
    {
		/// <summary>
		/// Dodaje menu g³ównego poziomu (poziome) do okna aplikacji
		/// </summary>
		/// <param name="menuName">Identyfikator menu</param>
		/// <param name="header">Tekst wyœwietlany</param>
		/// <returns>Obiekt wpisu w menu, w zasadzie na zewn¹trz MenuManagera zbêdne</returns>
        MenuEntry AddTopLevelMenu(string menuName, string header);

		/// <summary>
		/// Dodaje wpis do menu (pionowego)
		/// </summary>
		/// <typeparam name="T">Typ eventa, który bêdzie publikowany po klikniêciu</typeparam>
		/// <param name="topLevelMenuName">Identyfikator menu g³ównego poziomu, do którego siê wpi¹æ</param>
		/// <param name="menuName">Identyfikator wpisu</param>
		/// <param name="header">Tekst wyœwietlany</param>
		void AddItemToMenu<T, P>(string topLevelMenuName, string menuName, string header, CommandCanExecuteHelper canExecute)
			where T : CompositePresentationEvent<P>;

		void AddItemToMenu<T>(string topLevelMenuName, string menuName, string header, CommandCanExecuteHelper canExecute)
			where T : CompositePresentationEvent<object>;

		void AddItemToMenu<T, P>(string topLevelMenuName, string menuName, string header)
			where T : CompositePresentationEvent<P>;

		void AddItemToMenu<T>(string topLevelMenuName, string menuName, string header)
			where T : CompositePresentationEvent<object>;

    	void AddLabeledCommand<TCommand, TEvent, TArgmunet>()
    		where TCommand : LabeledEventPublisherCommand<TEvent, TArgmunet>
    		where TEvent : CompositePresentationEvent<TArgmunet>;

    }
}