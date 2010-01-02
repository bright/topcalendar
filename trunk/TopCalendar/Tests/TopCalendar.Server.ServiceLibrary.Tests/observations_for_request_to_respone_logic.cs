using System;
using Microsoft.Practices.ServiceLocation;
using NHibernate;
using Rhino.Mocks;
using TopCalendar.Server.DataLayer;
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
			var transactionWorker = Dependency<IWithinTransactionWorker>();
			transactionWorker.Stub(worker => worker.Perform(null)).IgnoreArguments()
				.WhenCalled(mi => ((Action)mi.Arguments[0])() );	
			ProvideBindingOf<ITransactionHandler<TResponse>,TransactionHandler<TResponse>>();
		}
	}
}