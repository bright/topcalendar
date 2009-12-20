#region

using System;
using Microsoft.Practices.ServiceLocation;
using NHibernate;
using TopCalendar.Server.ServiceLibrary.ServiceContract.DataContract;
using TopCalendar.Server.ServiceLibrary.ServiceContract.DataContract.StatusReason;

#endregion

namespace TopCalendar.Server.ServiceLibrary.ServiceLogic
{
    public abstract class RequestToResponseLogic<TRequest, TResponse> where TResponse : BaseResponse, new()
    {    	    	
		protected ITransactionHandler<TResponse> WithinTransactionDo(Action<IServiceLocator> doJob)
		{
			return new TransactionHandler<TResponse>(doJob);						
		}
    }

	public class TransactionHandler<TResponse> : ITransactionHandler<TResponse>
		where TResponse : BaseResponse, new() 
	{
		private readonly Action<IServiceLocator> _action;
		private ISession _session;
		private IServiceLocator _serviceLocator;

		public TransactionHandler(Action<IServiceLocator> action)
		{
			_action = action;
			_serviceLocator = ServiceLocator.Current;
			_session = _serviceLocator.GetInstance<ISession>();
		}

        protected void WithinTransactionDo(Action doJob)
        {
			using(var t = _session.BeginTransaction())
			{
				doJob();
				t.Commit();
			}
        }

		public TResponse OnErrorSetMessage(string message)
		{
			try
			{
				WithinTransactionDo(()=> _action(_serviceLocator));
				return SuccessSituationResponse();
			}catch
			{
				return ErrorSituationResponse(message);
			}
		}

		public TResponse OnErrorFillResponse(Action<TResponse> setThings)
		{
			try
			{
				WithinTransactionDo(() => _action(_serviceLocator));
				return SuccessSituationResponse();
			}catch(Exception ex)
			{
				var resposne = ErrorSituationResponse(ex.Message);
				setThings(resposne);
				return resposne;
			}
		}

		public TResponse OnErrorFillResposneWithException()
		{
			try
			{
				WithinTransactionDo(() => _action(_serviceLocator));
				return SuccessSituationResponse();
			}catch(Exception ex)
			{
				return ErrorSituationResponse(ex.Message);
			}
		}

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

	public interface ITransactionHandler<TResponse>
		where TResponse : BaseResponse, new() 		
	{
		TResponse OnErrorSetMessage(string message);
		TResponse OnErrorFillResponse(Action<TResponse> response);
		TResponse OnErrorFillResposneWithException();
	}
}