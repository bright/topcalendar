using System;
using Microsoft.Practices.Composite.Events;
using Ninject;
using NUnit.Framework;
using Rhino.Mocks;
using TopCalendar.Client.DataModel;
using TopCalendar.UI.Infrastructure;
using TopCalendar.UI.PluginManager;
using TopCalendar.Utility.Tests;
using TopCalendar.Utility.UI;

namespace TopCalendar.UI.Modules.TaskViewer.Tests
{

    public abstract class observation_for_task_viewer_module : observations_for_auto_created_sut_of_type<TaskViewerModule>
    {

        protected ShowAddNewTaskViewEvent _showAddNewTaskEvent;
        protected ShowEditTaskViewEvent _showEditTaskEvent;

        protected ITaskView _taskView;

        protected override void AfterSutCreation()
        {
            Sut.Initialize();
        }

        protected override void EstablishContext()
        {

            IEventAggregator eventAggregator = Dependency<IEventAggregator>();

            _showAddNewTaskEvent = new ShowAddNewTaskViewEvent();
            _showEditTaskEvent = new ShowEditTaskViewEvent();

            eventAggregator.Stub(aggregator => aggregator.GetEvent<ShowAddNewTaskViewEvent>()).Return(_showAddNewTaskEvent);

            eventAggregator.Stub(aggregator => aggregator.GetEvent<ShowEditTaskViewEvent>()).Return(_showEditTaskEvent);

            eventAggregator.Stub(aggregator =>
                aggregator.GetEvent<RegistrationCompletedEvent>()).Return(new RegistrationCompletedEvent());

            _taskView = Dependency<ITaskView>();
            Dependency<ITaskPresentationModel>().Stub(x => x.View).Return(_taskView);
        }
    }

    [TestFixture]
    public class TaskViewerModule_when_ShowAddNewTaskViewEvent_is_published
        : observation_for_task_viewer_module
    {
        protected DateTime _startDate;

        protected override void EstablishContext()
        {
            base.EstablishContext();
            _startDate = DateTime.Now;
        }

        protected override void Because()
        {
            _showAddNewTaskEvent.Publish(_startDate);
        }

        [Test]
        public void task_view_should_be_registered_with_MainContent_region()
        {
            Dependency<IPluginLoader>().AssertWasCalled(loader =>
                loader.RegisterViewWithRegion(
                    Arg<string>.Is.Equal(RegionNames.MainContent),
                    Arg<IView>.Is.Equal(_taskView)
                )
            );
        }
    }

    [TestFixture]
    public class TaskViewerModule_when_ShowEditTaskViewEvent_is_published : observation_for_task_viewer_module
    {
        private Task _taskToEdit;

        protected override void EstablishContext()
        {
            base.EstablishContext();
            _taskToEdit = new Task("name", DateTime.Now);
        }

        protected override void Because()
        {
            _showEditTaskEvent.Publish(_taskToEdit);
        }

        [Test]
        public void task_view_should_be_registered_with_MainContent_region()
        {
            Dependency<IPluginLoader>().AssertWasCalled(loader =>
                loader.RegisterViewWithRegion(
                    Arg<string>.Is.Equal(RegionNames.MainContent),
                    Arg<IView>.Is.Equal(_taskView)
                )
            );
        }

    }
}
