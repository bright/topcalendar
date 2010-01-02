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
		: ResponseLogic<TResponse>
		where TResponse : BaseResponse, new()
    {    	    	
		protected ITransactionHandler<TResponse> WithinTransactionDo(Action<IServiceLocator> doJob)
		{
			var kenel = ServiceLocator.Current.GetInstance<IKernel>();
			return kenel.Get<ITransactionHandler<TResponse>>(new ConstructorArgument("action", doJob));			
		}

    }	
}