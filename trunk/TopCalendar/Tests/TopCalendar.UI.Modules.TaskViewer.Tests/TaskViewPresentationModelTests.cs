using System;
using Microsoft.Practices.Composite.Events;
using Ninject;
using NUnit.Framework;
using Rhino.Mocks;
using TopCalendar.Client.Connector;
using TopCalendar.Client.DataModel;
using TopCalendar.UI.Infrastructure;
using TopCalendar.UI.PluginManager;
using TopCalendar.Utility.Tests;
using TopCalendar.Utility.UI;

namespace TopCalendar.UI.Modules.TaskViewer.Tests
{
    public abstract class observations_for_task_view_presentation_model : observations_for_auto_created_sut_of_type_with_eventaggregator<TaskPresentationModel>
    {
		protected DeactivateViewEvent DeactivateViewEvent;
        protected ITaskView _taskView;
    	private IView _argument;

    	protected override void EstablishContext()
        {
            base.EstablishContext();
        	DeactivateViewEvent = EventAggr.GetEvent<DeactivateViewEvent>();
        	DeactivateViewEvent.Subscribe(_unloadViewEventAction);
            _taskView = Dependency<ITaskView>();
        }

    	private void _unloadViewEventAction(IView obj)
    	{
    		_argument = obj;
    	}

    	protected void assert_unload_view_event_was_called_with_argument<TType>(TType argument)
		{
    		_argument.ShouldEqual(argument);
		}

		protected void assert_unload_view_event_was_not_called_with_argument<TType>(TType argument)
		{
			_argument.ShouldNotEqual(argument);
		}


    }

    
    public class TaskViewPresentationModel_when_CancelCommand_is_executed : observations_for_task_view_presentation_model
    {

        protected override void Because()
        {
            Sut.CancelCommand.Execute(null);
        }

        [Test]
        public void View_should_be_unloaded()
        {
            assert_unload_view_event_was_called_with_argument(Dependency<ITaskView>());
        }
    }

    
    public class TaskViewPresentationModel_when_UpdateCommand_is_executed : observations_for_task_view_presentation_model
    {

        protected override void Because()
        {
            Sut.UpdateCommand.Execute(null);
        }

        [Test]
        public void UpdateTask_method_in_TaskRepository_should_be_called()
        {
            Dependency<ITaskRepository>().AssertWasCalled(x => x.UpdateTask(Arg<Task>.Is.Anything));
        }
    }

    
    public class TaskViewPresentationModel_when_UpdateCommand_is_executed_and_UpdateTask_ended_successfull
        : observations_for_task_view_presentation_model
    {

        protected override void EstablishContext()
        {
            base.EstablishContext();
            Dependency<ITaskRepository>().Stub(repo => repo.UpdateTask(null))
                .IgnoreArguments().Return(true);
        }

        protected override void Because()
        {
            Sut.UpdateCommand.Execute(null);
        }
        [Test]
        public void TaskView_should_be_unregistered()
        {
			assert_unload_view_event_was_called_with_argument(_taskView);
        }
    }

    
    public class TaskViewPresentationModel_when_UpdateCommand_is_executed_and_UpdateTask_fail
        : observations_for_task_view_presentation_model
    {

        protected override void EstablishContext()
        {
            base.EstablishContext();
            Dependency<ITaskRepository>().Stub(repo => repo.UpdateTask(null))
                .IgnoreArguments().Return(false);
        }

        protected override void Because()
        {
            Sut.UpdateCommand.Execute(null);
        }
        [Test]
        public void TaskView_should_not_be_unregistered()
        {
			assert_unload_view_event_was_not_called_with_argument(_taskView);
        }
    }

    
    public class TaskPresentationModel_when_AddCommand_is_executed : observations_for_task_view_presentation_model
    {
        protected override void Because()
        {
            Sut.AddCommand.Execute(null);
        }

        [Test]
        public void AddTask_in_TaskRepository_should_be_called ()
        {
            Dependency<ITaskRepository>().AssertWasCalled(x => x.AddTask(Arg<Task>.Is.Anything));
        }
    }

    
    public class TaskPresentationModel_when_AddCommand_is_executed_and_AddTask_success : observations_for_task_view_presentation_model
    {
        protected override void EstablishContext()
        {
            base.EstablishContext();
            Dependency<ITaskRepository>().Stub(x => x.AddTask(null)).IgnoreArguments().Return(true);
        }

        protected override void Because()
        {
            Sut.AddCommand.Execute(null);
        }

        [Test]
        public void TaskView_should_be_unregistered()
        {
            assert_unload_view_event_was_called_with_argument(_taskView);
        }
    }

    
    public class TaskPresentationModel_when_AddCommand_is_executed_and_AddTask_fail : observations_for_task_view_presentation_model
    {
        protected override void EstablishContext()
        {
            base.EstablishContext();
            Dependency<ITaskRepository>().Stub(x => x.AddTask(null)).IgnoreArguments().Return(false);
        }

        protected override void Because()
        {
            Sut.AddCommand.Execute(null);
        }

        [Test]
        public void TaskView_should_not_be_unregistered()
        {
			assert_unload_view_event_was_not_called_with_argument(_taskView);
        }
    }



}
