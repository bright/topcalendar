using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Composite.Presentation.Events;
using Ninject;
using TopCalendar.UI.Infrastructure.CommonCommands;
using TopCalendar.UI.MenuInfrastructure;

namespace TopCalendar.UI.MenuInfrastructure
{
    public class MenuManager : IMenuManager
    {
        private readonly IServiceLocator _serviceLocator;
        private readonly IMenuProvider _menuProvider;

        public MenuManager()
        {
            _serviceLocator = ServiceLocator.Current;
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

        public void AddItemToMenu<T, P>(string topLevelMenuName, string menuName, string header, CommandCanExecuteHelper canExecute)
            where T : CompositePresentationEvent<P>
        {
            var topLevel = _menuProvider.GetTopLevelMenu(topLevelMenuName) ?? AddTopLevelMenu(topLevelMenuName, header);

        	_menuProvider.AddItemToMenu(topLevel, new MenuEntry()
                                   {
                                       Name = menuName,
                                       Header = header,
                                       Command = new MenuEventCommand<T, P>(_serviceLocator, canExecute)
                                   });
        }

		public void AddItemToMenu<T>(string topLevelMenuName, string menuName, string header, CommandCanExecuteHelper canExecute)
			where T : CompositePresentationEvent<object>
		{
			AddItemToMenu<T, object>(topLevelMenuName, menuName, header, canExecute);
		}

		public void AddItemToMenu<T, P>(string topLevelMenuName, string menuName, string header)
			where T : CompositePresentationEvent<P>
		{
			AddItemToMenu<T, P>(topLevelMenuName, menuName, header, null);
		}

		public void AddItemToMenu<T>(string topLevelMenuName, string menuName, string header)
			where T : CompositePresentationEvent<object>
		{
			AddItemToMenu<T>(topLevelMenuName, menuName, header, null);
		}


    	public void AddLabeledCommand<TCommand, TEvent, TArgmunet>() 
			where TCommand: LabeledEventPublisherCommand<TEvent,TArgmunet>
			where TEvent : CompositePresentationEvent<TArgmunet>
    	{
    		//todo: refactor this
    		var kernel = _serviceLocator.GetInstance<IKernel>();
    		kernel.Bind<ILabeledCommand<TArgmunet>>().To<TCommand>();
    	}
    }	
}
