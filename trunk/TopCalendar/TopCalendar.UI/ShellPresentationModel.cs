using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Controls;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.ServiceLocation;
using TopCalendar.UI.Infrastructure;
using TopCalendar.UI.MenuInfrastructure;
using TopCalendar.UI.Modules.Registration;
using TopCalendar.Utility.UI;

namespace TopCalendar.UI
{
	public class ShellPresentationModel : NotifyPropertyChanged
	{
	    private readonly IMenuProvider _menuProvider;
	    private readonly IServiceLocator _serviceLocator;
		private readonly IShellController _shellController;

		public ShellPresentationModel(IServiceLocator serviceLocator, IRegionManager regionManager, IShellController shellController)
	    {
	        _serviceLocator = serviceLocator;
			_shellController = shellController;
			_menuProvider = _serviceLocator.GetInstance<IMenuProvider>();

	    	AddDefaultMenuEntries();
	        SubscribeToDefaultEvents();

			// observe changes in menu
			_menuProvider.Menus.CollectionChanged += MenusCollectionChanged;
	    }

		private void AddDefaultMenuEntries()
		{
			var menu = _serviceLocator.GetInstance<IMenuManager>();
			menu.AddTopLevelMenu("Program", "Program");
			menu.AddItemToMenu<CloseAppEvent>("Program", "Finish", "Zakoñcz");
			menu.AddTopLevelMenu("TasksMenu", "Zadania");
		}

		private void SubscribeToDefaultEvents()
	    {
	        var eventAggregator = _serviceLocator.GetInstance<IEventAggregator>();

            // close app event
	        eventAggregator.GetEvent<CloseAppEvent>().Subscribe(
                f => _serviceLocator.GetInstance<IShellView>().Close()
            );

			eventAggregator.GetEvent<ViewCompleted<IRegistrationView>>().Subscribe(view => _shellController.ViewCompleted(view, RegionNames.MainContent));
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

		private void MenusCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			OnPropertyChanged("MainMenu");
		}
	}
}