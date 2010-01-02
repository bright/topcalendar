using System;
using TopCalendar.Server.ServiceLibrary.ServiceContract.DataContract;
using TopCalendar.Server.ServiceLibrary.ServiceContract.DataContract.StatusReason;

namespace TopCalendar.Server.ServiceLibrary.ServiceLogic
{
	public abstract class ResponseLogic<TResponse> where TResponse : BaseResponse, new()
	{
		public static TResponse ErrorSituationResponse(String statusReason)
		{
			return Response(statusReason, false);
		}

		public static TResponse SuccessSituationResponse()
		{
			return Response(StatusReasonFor.All.OK, true);
		}

		private static TResponse Response(String statusReason, Boolean success)
		{
			TResponse response = new TResponse
			                     	{
			                     		StatusReason = statusReason,
			                     		Success = success
			                     	};
			return response;
		}
	}
}