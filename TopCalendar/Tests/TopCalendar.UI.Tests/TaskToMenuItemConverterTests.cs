using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Microsoft.Practices.Composite.Presentation.Commands;
using NUnit.Framework;
using Rhino.Mocks;
using TopCalendar.Client.DataModel;
using TopCalendar.UI.Infrastructure.CommonCommands;
using TopCalendar.Utility.Tests;

namespace TopCalendar.UI.Tests
{
	public class when_converting_task_to_menu_item : observations_for_auto_created_sut_of_type<TaskToMenuItemConverter>
	{
		private Task _task;
		private IEnumerable<MenuItem> _result;
		private ILabeledCommand<Task> _command;
		private string _header;
		private DelegateCommand<Task> _deleagateCommand;

		protected override void Because()
		{
			_result = Sut.Convert(_task, null, null, null) as IEnumerable<MenuItem>;
		}

		protected override void EstablishContext()
		{
			base.EstablishContext();
			_task = new Task();
			_command = Dependency<ILabeledCommand<Task>>();
			_header = Guid.NewGuid().ToString();
			_deleagateCommand = new DelegateCommand<Task>(t=> { });
			_command.Stub(c => c.Header).Return(_header);
			_command.Stub(c => c.Command).Return(_deleagateCommand);
		}

		[Test]
		public void should_return_proper_list()
		{
			_result.ShouldNotBeNull();	
		}

		[Test]
		public void should_return_proper_menu_item()
		{
			_result.ShouldContain(m=> m.Header.Equals(_header) && m.Command.Equals(_deleagateCommand) && m.CommandParameter.Equals(_task) );
		}

		[Test]
		public void should_return_proper_count()
		{
			_result.ShouldHaveCount(1);
		}
	}
}