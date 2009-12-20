using System;
using Microsoft.Practices.ServiceLocation;
using NHibernate;
using Rhino.Mocks;
using TopCalendar.Server.ServiceLibrary.ServiceContract.DataContract;
using TopCalendar.Server.ServiceLibrary.ServiceLogic;
using TopCalendar.Utility.Tests;

namespace TopCalendar.Server.ServiceLibrary.Tests
{
	public abstract class observations_for_request_to_respone_logic<TType,TRequest,TResponse>:
		observations_for_auto_created_sut_of_type<TType>
		where TType : RequestToResponseLogic<TRequest,TResponse> where TResponse : BaseResponse, new()
	{
		protected override void EstablishContext()
		{			
			ProvideBindingOf<ITransactionHandler<TResponse>,FakeTransactionHandler<TResponse>>();
		}
	}

	public class FakeTransactionHandler<TResponse> : ITransactionHandler<TResponse>
		where TResponse : BaseResponse, new()
	{
		private readonly Action<IServiceLocator> _action;
		private IServiceLocator _serviceLocator;

		public FakeTransactionHandler(Action<IServiceLocator> action)
		{
			_action = action;
			_serviceLocator = ServiceLocator.Current;
		}

		public TResponse OnErrorSetMessage(string message)
		{
			try
			{
				_action(_serviceLocator);
				return ResponseLogic<TResponse>.SuccessSituationResponse();
			}catch
			{
				return ResponseLogic<TResponse>.ErrorSituationResponse(message);
			}
		}

		public TResponse OnErrorFillResponse(Action<TResponse> setResponse)
		{
			try
			{
				_action(_serviceLocator);
				return ResponseLogic<TResponse>.SuccessSituationResponse();
			}catch(Exception ex)
			{
				var response = ResponseLogic<TResponse>.ErrorSituationResponse(ex.Message);
				setResponse(response);
				return response;
			}
		}

		public TResponse OnErrorFillResposneWithException()
		{
			try
			{
				_action(_serviceLocator);
				return ResponseLogic<TResponse>.SuccessSituationResponse();
			}
			catch (Exception ex)
			{
				return ResponseLogic<TResponse>.ErrorSituationResponse(ex.Message);								
			}
		}
	}
}