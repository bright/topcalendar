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
    public class TaskViewerModule_when_ShowAddNewTaskViewEvent_is_published :observations_for_auto_created_sut_of_type <TaskViewerModule>{
        protected DateTime _startDate;
        private ITaskPresentationModel _presentationModel;
        private ITaskView _taskView;

        protected override void Because()
        {
            Dependency<ShowAddNewTaskViewEvent>().Publish(_startDate);
        }
        protected override void AfterSutCreation()
        {
            base.AfterSutCreation();
            Sut.Initialize();
        }
        protected override void EstablishContext()
        {
            _startDate = DateTime.Now;
            IEventAggregator eventAggregator = Dependency<IEventAggregator>();
            IKernel kernel = Dependency<IKernel>();
            IPluginLoader pluginLoader = Dependency<IPluginLoader>();

            ShowAddNewTaskViewEvent showAddNewTaskViewEvent = MockRepository.GenerateStub<ShowAddNewTaskViewEvent>();

            eventAggregator.Stub(aggregator => 
                aggregator.GetEvent<ShowAddNewTaskViewEvent>()).Return(
                    showAddNewTaskViewEvent);

            _presentationModel = MockRepository.GenerateStub<ITaskPresentationModel>();

            kernel.Stub(x => x.Get<ITaskPresentationModel>()).Return(_presentationModel);

            _taskView = MockRepository.GenerateStub<ITaskView>();
            _presentationModel.Stub(x => x.View).Return(_taskView);
        }

        [Test]
        public void task_view_should_be_registered_with_MainContent_region()
        {
            Dependency<IPluginLoader>().AssertWasCalled(loader =>
                                                        loader.RegisterViewWithRegion(
                                                            Arg<string>.Is.Equal(RegionNames.MainContent),
                                                            Arg<IView>.Is.Equal(_taskView))
                                                            );
        }
    }
}
