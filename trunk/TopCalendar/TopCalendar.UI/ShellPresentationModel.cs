using System.Collections.ObjectModel;
using System.Windows.Controls;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.ServiceLocation;
using TopCalendar.UI.Infrastructure;
using TopCalendar.UI.MenuInfrastructure;

namespace TopCalendar.UI
{
	public class ShellPresentationModel
	{
	    private readonly IMenuProvider _menuProvider;
	    private readonly IServiceLocator _serviceLocator;

	    public ShellPresentationModel(IServiceLocator serviceLocator)
	    {
	        _serviceLocator = serviceLocator;
            _menuProvider = _serviceLocator.GetInstance<IMenuProvider>();

	        SubscribeToDefaultEvents();
	    }

	    private void SubscribeToDefaultEvents()
	    {
	        var eventAggregator = _serviceLocator.GetInstance<IEventAggregator>();

            // close app event
	        eventAggregator.GetEvent<CloseAppEvent>().Subscribe(
                f => _serviceLocator.GetInstance<IShellView>().Close()
            );
	    }

	    public ObservableCollection<MenuItem> MainMenu
	    {
	        get
	        {
	            var menuData = _menuProvider.Menus;

	            var menus = new ObservableCollection<MenuItem>();
	            foreach (var menuEntry in menuData)
	            {
	                menus.Add(menuEntry.AsMenuItem());
	            }

	            return menus;
	        }
	    }
	}
}