using Microsoft.Practices.Composite.Presentation.Events;

namespace TopCalendar.UI.MenuInfrastructure
{
    public interface IMenuManager
    {
        MenuEntry AddTopLevelMenu(string menuName, string header);

        void AddItemToMenu<T>(string topLevelMenuName, string menuName, string header) 
            where T : CompositePresentationEvent<object>;
    }
}