using System.Collections.ObjectModel;

namespace TopCalendar.UI.MenuInfrastructure
{
    public interface IMenuProvider
    {
        ObservableCollection<MenuEntry> Menus { get; }

        MenuEntry GetTopLevelMenu(string name);
        MenuEntry AddTopLevelMenu(MenuEntry item);
        void AddItemToMenu(MenuEntry topLevel, MenuEntry newItem);
    }
}