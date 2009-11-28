using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Composite.Presentation.Events;

namespace TopCalendar.UI.MenuInfrastructure
{
    public class MenuManager : IMenuManager
    {
        private readonly IServiceLocator _serviceLocator;
        private readonly IMenuProvider _menuProvider;

        public MenuManager(IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
            _menuProvider = _serviceLocator.GetInstance<IMenuProvider>();
        }

        public MenuEntry AddTopLevelMenu(string menuName, string header)
        {
            return _menuProvider.AddTopLevelMenu(new MenuEntry()
                                        {
                                            Name = menuName, 
                                            Header = header
                                        });
        }

        public void AddItemToMenu<T, P>(string topLevelMenuName, string menuName, string header)
            where T : CompositePresentationEvent<P>
        {
            var topLevel = _menuProvider.GetTopLevelMenu(topLevelMenuName) ?? AddTopLevelMenu(topLevelMenuName, header);

        	_menuProvider.AddItemToMenu(topLevel, new MenuEntry()
                                   {
                                       Name = menuName,
                                       Header = header,
                                       Command = new MenuEventCommand<T, P>(_serviceLocator)
                                   });
        }

		public void AddItemToMenu<T>(string topLevelMenuName, string menuName, string header)
			where T : CompositePresentationEvent<object>
		{
			AddItemToMenu<T, object>(topLevelMenuName, menuName, header);
		}
    }
}
