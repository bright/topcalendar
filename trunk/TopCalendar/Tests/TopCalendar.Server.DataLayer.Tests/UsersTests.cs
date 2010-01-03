using System;
using NUnit.Framework;
using TopCalendar.Server.DataLayer.Entities;
using TopCalendar.Utility.BasicExtensions;
using TopCalendar.Utility.Tests;

namespace TopCalendar.Server.DataLayer.Tests
{
	public class when_creating_user_without_password : observations_for_sut
 	{
		private Exception _exception;

		protected override void Because()
		{
			_exception = ((Action)(()=> new User(Guid.NewGuid().ToString(),null))).ThrownException();
		}

		[Test]
		public void should_not_allow_to_create()
		{
			_exception.ShouldNotBeNull();
		}
	}
}