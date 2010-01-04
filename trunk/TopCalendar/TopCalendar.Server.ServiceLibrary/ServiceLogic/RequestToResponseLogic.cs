#region

using System;
using Microsoft.Practices.ServiceLocation;
using NHibernate;
using Ninject;
using Ninject.Parameters;
using TopCalendar.Server.ServiceLibrary.ServiceContract.DataContract;

#endregion

namespace TopCalendar.Server.ServiceLibrary.ServiceLogic
{
    public abstract class RequestToResponseLogic<TRequest, TResponse>
		: ResponseLogic<TResponse>, IRequestToResponseLogic<TRequest, TResponse>
		where TResponse : BaseResponse, new()
    {    	    	
		protected ITransactionHandler<TResponse> WithinTransactionDo(Action<IServiceLocator> doJob)
		{
			var kenel = ServiceLocator.Current.GetInstance<IKernel>();
			return kenel.Get<ITransactionHandler<TResponse>>(new ConstructorArgument("action", doJob));			
		}    	
    	public abstract TResponse Process(TRequest request);    	
    }

	public interface IRequestToResponseLogic<TRequest, TResponse>
	{
		TResponse Process(TRequest request);
	}
}