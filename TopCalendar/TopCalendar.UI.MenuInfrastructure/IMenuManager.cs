using Microsoft.Practices.Composite.Presentation.Events;

namespace TopCalendar.UI.MenuInfrastructure
{
    public interface IMenuManager
    {
		/// <summary>
		/// Dodaje menu g��wnego poziomu (poziome) do okna aplikacji
		/// </summary>
		/// <param name="menuName">Identyfikator menu</param>
		/// <param name="header">Tekst wy�wietlany</param>
		/// <returns>Obiekt wpisu w menu, w zasadzie na zewn�trz MenuManagera zb�dne</returns>
        MenuEntry AddTopLevelMenu(string menuName, string header);

		/// <summary>
		/// Dodaje wpis do menu (pionowego)
		/// </summary>
		/// <typeparam name="T">Typ eventa, kt�ry b�dzie publikowany po klikni�ciu</typeparam>
		/// <param name="topLevelMenuName">Identyfikator menu g��wnego poziomu, do kt�rego si� wpi��</param>
		/// <param name="menuName">Identyfikator wpisu</param>
		/// <param name="header">Tekst wy�wietlany</param>
        void AddItemToMenu<T>(string topLevelMenuName, string menuName, string header) 
            where T : CompositePresentationEvent<object>;
    }
}