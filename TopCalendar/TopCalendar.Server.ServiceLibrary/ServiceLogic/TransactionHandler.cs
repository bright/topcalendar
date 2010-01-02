using System;
using Microsoft.Practices.ServiceLocation;
using TopCalendar.Server.DataLayer;
using TopCalendar.Server.ServiceLibrary.ServiceContract.DataContract;

namespace TopCalendar.Server.ServiceLibrary.ServiceLogic
{
	//Todo: Consider refactoring of this class	
	public class TransactionHandler<TResponse> : ITransactionHandler<TResponse>
		where TResponse : BaseResponse, new() 
	{
		private readonly IWithinTransactionWorker _transactionWorker;
		private readonly Action<IServiceLocator> _action;
		
		private IServiceLocator _serviceLocator;

		public TransactionHandler(IWithinTransactionWorker transactionWorker, Action<IServiceLocator> action)
		{
			_transactionWorker = transactionWorker;
			_action = action;
		}

		protected void WithinTransactionDo(Action doJob)
		{
			_transactionWorker.Perform(doJob);
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
}