using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Practices.ServiceLocation;
using TopCalendar.UI.Infrastructure;

namespace TopCalendar.UI.MenuInfrastructure
{
    public class MenuProvider : IMenuProvider
    {
        private readonly IServiceLocator _serviceLocator;
        private readonly Dictionary<string, MenuEntry> _itemsDict;

        public MenuProvider(IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;

            Menus = new ObservableCollection<MenuEntry>();
            _itemsDict = new Dictionary<string, MenuEntry>();

            SetDefaultMenu();
        }

        private void SetDefaultMenu()
        {
            var appMenu = AddTopLevelMenu(new MenuEntry()
                              {
                                  Name = "AppMenu", 
                                  Header = "Program"
                              });

            AddItemToMenu(appMenu, new MenuEntry()
                                  {
                                      Header = "Zakoñcz",
                                      Command = new MenuEventCommand<CloseAppEvent>(_serviceLocator)
                                  });

            AddTopLevelMenu(new MenuEntry()
                            {
                                Name = "TasksMenu",
                                Header = "Zadania"
                            });
        }

        public ObservableCollection<MenuEntry> Menus { get; set; }

        public MenuEntry GetTopLevelMenu(string name)
        {
            if (_itemsDict.ContainsKey(name))
            {
                return _itemsDict[name];
            }

            return null;
        }

        public MenuEntry AddTopLevelMenu(MenuEntry item)
        {
            if (item != null)
            {
                Menus.Add(item);
                _itemsDict[item.Name] = item;
            }

            return item;
        }

        public void AddItemToMenu(MenuEntry topLevel, MenuEntry newItem)
        {
            if (newItem != null)
            {
                topLevel.Items.Add(newItem);
            }
        }
    }
}