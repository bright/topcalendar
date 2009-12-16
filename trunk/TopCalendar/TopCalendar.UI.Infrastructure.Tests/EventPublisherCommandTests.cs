using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Presentation.Events;
using NUnit.Framework;
using Rhino.Mocks;
using TopCalendar.UI.Infrastructure.CommonCommands;
using TopCalendar.Utility.Tests;

namespace TopCalendar.UI.Infrastructure.Tests
{
	public class DumbEvent : CompositePresentationEvent<string>
	{}

	public class DumbCommand : EventPublisherCommand<DumbEvent,string>
	{
		
	}
	
	public class when_executing_command_of_event_publisher_command : observations_for_auto_created_sut_of_type<DumbCommand>
	{
		private string _argument;
		private DumbEvent _dumbEvent;
		private string _publishedWithArgument;

		protected override void Because()
		{
			Sut.Execute(_argument);
		}

		protected override void EstablishContext()
		{
			base.EstablishContext();
			_publishedWithArgument = Guid.NewGuid().ToString();
			_dumbEvent = new DumbEvent();
			_dumbEvent.Subscribe(s => _publishedWithArgument = s);
			Dependency<IEventAggregator>().Stub(aggergator => aggergator.GetEvent<DumbEvent>()).IgnoreArguments().Return(_dumbEvent);
		}

		[Test]
		public void should_publish_event_with_proper_argument()
		{
			_publishedWithArgument.ShouldEqual(_argument);
		}
	}
}
