using System;
using Microsoft.Practices.Composite.Events;
using Ninject;
using NUnit.Framework;
using Rhino.Mocks;
using TopCalendar.UI.Infrastructure;
using TopCalendar.UI.PluginManager;
using TopCalendar.Utility.Tests;
using TopCalendar.Utility.UI;

namespace TopCalendar.UI.Modules.TaskViewer.Tests
{
    [TestFixture]
    public class TaskViewerModule_when_ShowAddNewTaskViewEvent_is_published
		: observations_for_auto_created_sut_of_type <TaskViewerModule>
	{
        protected DateTime _startDate;

		private ShowAddNewTaskViewEvent _showEvent;
        private ITaskView _taskView;

        protected override void Because()
        {
            _showEvent.Publish(_startDate);
        }

        protected override void AfterSutCreation()
        {
            Sut.Initialize();
        }

        protected override void EstablishContext()
        {
            _startDate = DateTime.Now;
            IEventAggregator eventAggregator = Dependency<IEventAggregator>();
            IPluginLoader pluginLoader = Dependency<IPluginLoader>();

			_showEvent = new ShowAddNewTaskViewEvent();

            eventAggregator.Stub(aggregator => 
                aggregator.GetEvent<ShowAddNewTaskViewEvent>()).Return(_showEvent);
			eventAggregator.Stub(aggregator =>
				aggregator.GetEvent<RegistrationCompletedEvent>()).Return(new RegistrationCompletedEvent());

			_taskView = Dependency<ITaskView>();
			Dependency<ITaskPresentationModel>().Stub(x => x.View).Return(_taskView);
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
