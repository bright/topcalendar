#region

using System;
using TopCalendar.Server.ServiceLibrary.ServiceContract.DataContract;
using TopCalendar.Server.ServiceLibrary.ServiceContract.DataContract.StatusReason;

#endregion

namespace TopCalendar.Server.ServiceLibrary.ServiceLogic
{
    public abstract class RequestToResponseLogic<TRequest, TResponse> where TResponse : BaseResponse, new()
    {
        protected TResponse ErrorSituationResponse(String statusReason)
        {
            return Response(statusReason, false);
        }

        protected TResponse SuccessSituationResponse()
        {
            return Response(StatusReasonFor.All.OK, true);
        }

        private TResponse Response(String statusReason, Boolean success)
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