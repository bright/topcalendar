using System;
using NUnit.Framework;
using TopCalendar.Utility.BasicExtensions;
using Rhino.Mocks;

namespace TopCalendar.Utility.Tests
{
	[TestFixture]
	public class when_checking_guard_with_true
		: observations_for_sut
	{
		private Exception _thrownException;

		protected override void Because()
		{			
			_thrownException = ((Action) (() => 
				Check.Guard(true, "message"))).ThrownException<ArgumentException>();
		}

		[Test]
		public void should_throw_no_exception()
		{
			_thrownException.ShouldBeNull();
		}
	}

	[TestFixture]
	public class when_checing_guard_with_false
		: observations_for_sut
	{
		private string _message;
		private Exception _thrownException;

		protected override void Because()
		{
			_thrownException = ((Action) (() =>
			                              Check.Guard(false, _message))).ThrownException();
		}

		protected override void EstablishContext()
		{
			_message = "SomeMessage";
		}

		[Test]
		public void should_return_throw_exception()
		{
			_thrownException.ShouldNotBeNull();
		}

		[Test]
		public void thrown_exception_is_argument_exception()
		{
			_thrownException.ShouldBeOfType<ArgumentException>();
		}

		[Test]
		public void should_return_proper_exception_message()
		{
			_thrownException.Message.ShouldEqual(_message);
		}
	}

	[TestFixture]
	public class when_checking_guard_with_function_returing_false
		: observations_for_sut
	{
		private string _message;
		private Func<bool> _boolFunc;
		private Exception _thrownException;

		protected override void Because()
		{
			_thrownException = ((Action) (() => Check.Guard<NullReferenceException>(_boolFunc, _message))).ThrownException();
		}

		protected override void EstablishContext()
		{
			_message = "SomeMessage";
			_boolFunc = () => false;
		}

		[Test]
		public void should_throw_proper_exception()
		{
			_thrownException.ShouldBeOfType<NullReferenceException>();
		}

		[Test]
		public void thrown_exception_should_have_proper_message()
		{
			_thrownException.Message.ShouldEqual(_message);
		}
	}

	[TestFixture]
	public class when_task_runner_executes_task
		: observations_for_auto_created_sut_of_type<TasksRunner>
	{
		protected override void Because()
		{			
			Sut.Execute(Dependency<IBootstrapperTask>());
		}

		[Test]
		public void should_call_execute_method_of_task()
		{
			Dependency<IBootstrapperTask>()
				.AssertWasCalled(task=> task.Execute());
		}
	}

	[TestFixture]
	public class when_task_runner_executes_task_with_type_given
		: observations_for_auto_created_sut_of_type<TasksRunner>
	{
		public class TestTask : IBootstrapperTask
		{
			public static bool WasExceuted = false;


			public void Execute()
			{
				WasExceuted = true;
			}
		}

		protected override void Because()
		{
			Sut.Execute<TestTask>();
		}

		[Test]
		public void should_execute_given_task()
		{
			TestTask.WasExceuted.ShouldBeTrue();
		}
	}

	
}