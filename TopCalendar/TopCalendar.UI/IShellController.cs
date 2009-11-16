using System;
using System.Collections.Generic;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.ServiceLocation;
using TopCalendar.UI.Infrastructure;
using TopCalendar.UI.Modules.MonthViewer;
using TopCalendar.UI.Modules.Registration;
using TopCalendar.Utility.UI;

namespace TopCalendar.UI
{
	public interface IShellController
	{
		void ViewCompleted<TView>(TView view, string regionName)
			where TView : IView;
	}

	class ShellController : IShellController
	{
		private readonly IRegionManager _regionManager;
		private readonly IServiceLocator _serviceLocator;

		private Dictionary<Type, Action> _viewCompletedActions = new Dictionary<Type, Action>();

		public ShellController(IRegionManager regionManager, IServiceLocator serviceLocator)
		{
			_regionManager = regionManager;
			_serviceLocator = serviceLocator;
			BuildViewCompletedActions();
		}

		private void BuildViewCompletedActions()
		{
			_viewCompletedActions[typeof (IRegistrationView)] = RegistrationCompleted;
		}

		private void RegistrationCompleted()
		{
			_regionManager.RegisterViewWithRegion(RegionNames.MainContent,
			                                      () => _serviceLocator.GetInstance<IMonthViewPresentationModel>().View);
		}


		public void ViewCompleted<TView>(TView view, string regionName)
			where TView: IView
		{
			_regionManager.Regions[regionName].Remove(view);
			Type t = typeof (TView);
			if (_viewCompletedActions.ContainsKey(t))
				_viewCompletedActions[t]();
		}
	}
}