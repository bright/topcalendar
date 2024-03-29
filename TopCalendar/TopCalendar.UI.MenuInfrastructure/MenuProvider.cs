using System.Collections.Generic;
using Microsoft.Practices.ServiceLocation;
using TopCalendar.UI.Infrastructure;
using TopCalendar.Utility;
using System.Collections.ObjectModel;

namespace TopCalendar.UI.MenuInfrastructure
{
    public class MenuProvider : IMenuProvider
    {
        private readonly IServiceLocator _serviceLocator;
		private readonly RefreshableObservableCollection<MenuEntry> _menus;
        private readonly Dictionary<string, MenuEntry> _itemsDict;

        public MenuProvider(IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;

            _menus = new RefreshableObservableCollection<MenuEntry>();
            _itemsDict = new Dictionary<string, MenuEntry>();
        }

        public ObservableCollection<MenuEntry> Menus
		{
			get
			{
				return (ObservableCollection<MenuEntry>)_menus;
			}
		}

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
                _menus.Add(item);
                _itemsDict[item.Name] = item;
            }

            return item;
        }

        public void AddItemToMenu(MenuEntry topLevel, MenuEntry newItem)
        {
            if (newItem != null)
            {
                topLevel.Items.Add(newItem);
				_menus.Refresh();
            }
        }
    }
}