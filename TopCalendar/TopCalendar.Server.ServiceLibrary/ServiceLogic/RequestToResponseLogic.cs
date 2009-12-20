#region

using System;
using Microsoft.Practices.ServiceLocation;
using NHibernate;
using Ninject;
using Ninject.Parameters;
using TopCalendar.Server.ServiceLibrary.ServiceContract.DataContract;
using TopCalendar.Server.ServiceLibrary.ServiceContract.DataContract.StatusReason;

#endregion

namespace TopCalendar.Server.ServiceLibrary.ServiceLogic
{
    public abstract class RequestToResponseLogic<TRequest, TResponse> 
		: ResponseLogic<TResponse>
		where TResponse : BaseResponse, new()
    {    	    	
		protected ITransactionHandler<TResponse> WithinTransactionDo(Action<IServiceLocator> doJob)
		{
			var kenel = ServiceLocator.Current.GetInstance<IKernel>();
			return kenel.Get<ITransactionHandler<TResponse>>(new ConstructorArgument("action", doJob));			
		}

    }

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
				return ResponseLogic<TResponse>.SuccessSituationResponse();
			}catch
			{
				return ResponseLogic<TResponse>.ErrorSituationResponse(message);
			}
		}

		public TResponse OnErrorFillResponse(Action<TResponse> setThings)
		{
			try
			{
				WithinTransactionDo(() => _action(_serviceLocator));
				return ResponseLogic<TResponse>.SuccessSituationResponse();
			}catch(Exception ex)
			{
				var resposne = ResponseLogic<TResponse>.ErrorSituationResponse(ex.Message);
				setThings(resposne);
				return resposne;
			}
		}

		public TResponse OnErrorFillResposneWithException()
		{
			try
			{
				WithinTransactionDo(() => _action(_serviceLocator));
				return ResponseLogic<TResponse>.SuccessSituationResponse();
			}catch(Exception ex)
			{
				return ResponseLogic<TResponse>.ErrorSituationResponse(ex.Message);
			}
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