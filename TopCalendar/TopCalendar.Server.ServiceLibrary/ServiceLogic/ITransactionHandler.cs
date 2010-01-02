using System;
using TopCalendar.Server.ServiceLibrary.ServiceContract.DataContract;

namespace TopCalendar.Server.ServiceLibrary.ServiceLogic
{
	public interface ITransactionHandler<TResponse>
		where TResponse : BaseResponse, new() 		
	{
		TResponse OnErrorSetMessage(string message);
		TResponse OnErrorFillResponse(Action<TResponse> response);
		TResponse OnErrorFillResposneWithException();
	}
}