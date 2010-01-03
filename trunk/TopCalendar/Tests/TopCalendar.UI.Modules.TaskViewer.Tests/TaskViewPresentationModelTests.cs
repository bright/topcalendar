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

    public class TaskPresentationModel_when_we_set_Task_property_and_FinishAt_is_null : observations_for_task_view_presentation_model
    {

        protected Task _task;
        protected DateTime _startAt = DateTime.Now;

        protected override void EstablishContext()
        {
            base.EstablishContext();
            _task = new Task() {FinishAt = null, StartAt = _startAt};
        }

        protected override void Because()
        {
            Sut.Task = _task;
        }

        [Test]
        public void IsEndDateEnabled_should_be_false()
        {
            Assert.IsFalse(Sut.IsEndDateEnabled);
        }
        [Test]
        public void difference_between_StartAt_and_displayed_FinishAtDate_should_equals_one_hour()
        {
            Assert.AreEqual(_startAt.AddHours(1),Sut.FinishAtDate);
        }
        [Test]
        public void FinishAt_should_not_be_changed()
        {
            Assert.IsNull(Sut.Task.FinishAt);
        }
    }

    public class TaskPresentationModel_when_we_set_Task_property_and_FinishAt_is_not_null : observations_for_task_view_presentation_model
    {

        protected Task _task;
        protected DateTime _startAt;
        protected DateTime _finishAt;
        protected override void EstablishContext()
        {
            base.EstablishContext();
            _startAt = DateTime.Now;
            _finishAt = _startAt.AddHours(10);

            _task = new Task() { FinishAt = _finishAt, StartAt = _startAt };
        }

        protected override void Because()
        {
            Sut.Task = _task;
        }

        [Test]
        public void IsEndDateEnabled_should_be_true()
        {
            Assert.IsTrue(Sut.IsEndDateEnabled);
        }
    }

    public class TaskPresentationModel_when_IsEndDateEnabled_is_false_and_we_set_to_true_and_FinishAt_is_null : observations_for_task_view_presentation_model
    {
        protected DateTime _startAt = DateTime.Now;

        protected override void Because()
        {
            Sut.IsEndDateEnabled = true;
        }
        protected override void AfterSutCreation()
        {
            Sut.Task = new Task() { StartAt = _startAt, FinishAt = null };
            Assert.IsFalse(Sut.IsEndDateEnabled,"Assumption failed. If FinishAt is null IsEndDateEnabled should be false.");
        }

        [Test]
        public void FinishAt_should_be_set_to_one_hour_after_StartAt()
        {
            Assert.AreEqual(Sut.Task.StartAt.AddHours(1),Sut.Task.FinishAt);
        }

        [Test]
        public void displayed_FinishAt_should_be_set_to_one_hour_after_StartAt()
        {
            Assert.AreEqual(Sut.Task.StartAt.AddHours(1), Sut.FinishAtDate);
        }
    }

    public class TaskPresentationModel_when_FinishAt_is_not_null_and_we_set_IsEndDateEnabled_from_true_to_false : observations_for_task_view_presentation_model
    {
        protected DateTime _startAt = DateTime.Now;
        protected DateTime _displayedDate;
        protected override void Because()
        {
            Sut.IsEndDateEnabled = false;
        }
        protected override void AfterSutCreation()
        {
            Sut.Task = new Task() { StartAt = _startAt, FinishAt = DateTime.Now.AddHours(2) };
            Assert.IsTrue(Sut.IsEndDateEnabled, "Assumption failed. If FinishAt is not null IsEndDateEnabled should be true.");
            _displayedDate = Sut.FinishAtDate;
        }

        [Test]
        public void FinishAt_should_be_set_to_null()
        {
            Assert.IsNull(Sut.Task.FinishAt);
        }
        [Test]
        public void displayed_FinishAt_date_should_not_be_changed()
        {
            Assert.AreEqual(_displayedDate,Sut.FinishAtDate);
        }
    }

    public class TaskPresentationModel_when_we_call_ShowAddNewTaskView : observations_for_task_view_presentation_model
    {
        protected DateTime _newDateTime;
        protected override void Because()
        {
            Sut.ShowAddNewTaskView(_newDateTime);
        }

        protected override void EstablishContext()
        {
            _newDateTime = DateTime.Now;
        }

        [Test]
        public void IsNewTask_should_be_true()
        {
            Assert.IsTrue(Sut.IsNewTask);
        }

        [Test]
        public void StartAt_date_should_be_equals_newDateTime()
        {
            Assert.AreEqual(Sut.Task.StartAt,_newDateTime);
        }
    }

    public class TaskPresentationModel_when_we_call_ShowEditTaskView : observations_for_task_view_presentation_model
    {
        protected Task _taskToEdit;
        protected override void Because()
        {
            Sut.ShowEditTaskView(_taskToEdit);
        }

        protected override void EstablishContext()
        {
            _taskToEdit = new Task(){Name = "some name",StartAt = DateTime.Now, FinishAt = DateTime.Now.AddHours(4)};
        }

        [Test]
        public void IsNewTask_should_be_false()
        {
            Assert.IsFalse((Sut.IsNewTask));
        }

        [Test]
        public void Task_name_should_be_same()
        {
            Assert.AreEqual(Sut.Task.Name, _taskToEdit.Name);
        }

        [Test]
        public void Task_StartAt_should_be_same()
        {
            Assert.AreEqual(Sut.Task.StartAt, _taskToEdit.StartAt);
        }
    }
}
