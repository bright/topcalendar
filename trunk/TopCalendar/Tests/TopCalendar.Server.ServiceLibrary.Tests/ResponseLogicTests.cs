using System;
using NUnit.Framework;
using TopCalendar.Server.ServiceLibrary.ServiceContract.DataContract;
using TopCalendar.Server.ServiceLibrary.ServiceLogic;
using TopCalendar.Utility.Tests;

namespace TopCalendar.Server.ServiceLibrary.Tests
{
	public class when_getting_response : observations_for_sut
	{
		protected override void Because()
		{			
		}

		[Test]
		public void should_return_proper_success_situation()
		{
			ResponseLogic<BaseResponse>.SuccessSituationResponse().Success.ShouldEqual(true);
		}

		[Test]
		public void should_return_proper_error_situation_response()
		{			
			ResponseLogic<BaseResponse>.ErrorSituationResponse(null).Success.ShouldEqual(false);
		}

		[Test]
		public void should_return_error_situation_resposne_with_proper_message()
		{
			var message = Guid.NewGuid().ToString();
			ResponseLogic<BaseResponse>.ErrorSituationResponse(message).StatusReason.ShouldEqual(message);
		}
	}
}