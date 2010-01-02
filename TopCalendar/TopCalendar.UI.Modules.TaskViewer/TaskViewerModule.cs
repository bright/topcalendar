using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Regions;
using Ninject;
using TopCalendar.Client.DataModel;
using TopCalendar.UI.Infrastructure;
using TopCalendar.UI.PluginManager;
using TopCalendar.Utility.UI;
using TopCalendar.UI.MenuInfrastructure;

namespace TopCalendar.UI.Modules.TaskViewer
{
    public class TaskViewerModule : IModule
    {
        private readonly IKernel _kernel;
        private readonly IPluginLoader _pluginLoader;
        private readonly IEventAggregator _eventAggregator;

		private readonly CommandCanExecuteHelper _canBeRun;

        public TaskViewerModule(IKernel kernel, IPluginLoader pluginLoader, IEventAggregator eventAggregator)
        {
            _kernel = kernel;
            _pluginLoader = pluginLoader;
            _eventAggregator = eventAggregator;

			_canBeRun = new CommandCanExecuteHelper(false);
        }

        public void Initialize()
        {
            RegisterViewsAndServices();

			_kernel.Get<IMenuManager>().AddItemToMenu<ShowAddNewTaskViewEvent, DateTime?>(
				"TasksMenu", "AddTask", "Dodaj zadanie", _canBeRun
			);
        }

        private void RegisterViewsAndServices()
        {
            _kernel.Bind<ITaskView>().To<TaskView>().InSingletonScope();
			_kernel.Bind<IPresentationModelFor<ITaskView>>().To<TaskPresentationModel>().InSingletonScope();
			_kernel.Bind<IViewForModel<ITaskView, TaskPresentationModel>>().To<TaskView>().InSingletonScope();
			_kernel.Bind<ITaskPresentationModel>().To<TaskPresentationModel>().InSingletonScope();

			_eventAggregator.GetEvent<RegistrationCompletedEvent>().Subscribe(HandleRegistrationCompletedEvent);
            _eventAggregator.GetEvent<ShowAddNewTaskViewEvent>().Subscribe(HandleShowAddNewTaskViewEvent);
            _eventAggregator.GetEvent<ShowEditTaskViewEvent>().Subscribe(HandleShowEditTaskEvent);
        }

		private void HandleRegistrationCompletedEvent(string login)
		{			
			var view = _kernel.Get<ITaskPresentationModel>().View;
			view.IsActiveChanged += (o, e) => _canBeRun.CanExecute = !view.IsActive;			                        	
			_pluginLoader.RegisterInActiveViewWithRegion( RegionNames.ModalPopupContent, view);						
		}

        private void HandleShowAddNewTaskViewEvent(DateTime? time)
        {
            var taskPresentationModel = _kernel.Get<ITaskPresentationModel>();
			_pluginLoader.ActivateView(RegionNames.ModalPopupContent, taskPresentationModel.View);
            taskPresentationModel.ShowAddNewTaskView(time);
        }

        private void HandleShowEditTaskEvent(Task taskToEdit)
        {
			var taskPresentationModel = _kernel.Get<ITaskPresentationModel>();
			_pluginLoader.ActivateView(RegionNames.ModalPopupContent, taskPresentationModel.View);
            taskPresentationModel.ShowEditTaskView(taskToEdit);
        }
    }
}
