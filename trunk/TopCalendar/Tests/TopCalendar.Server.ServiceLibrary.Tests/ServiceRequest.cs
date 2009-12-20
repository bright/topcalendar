using System;
using TopCalendar.Server.ServiceLibrary.ServiceContract.DataContract;

namespace TopCalendar.Server.ServiceLibrary.Tests
{
	public class ServiceRequest
	{
		public static TRequest Of<TRequest>(Action<TRequest> setRequest)
			where TRequest : RequestWithCredentials
		{
			var req = Activator.CreateInstance<TRequest>();
			req.UserCredentials = new UserCredentials {Login = Guid.NewGuid().ToString(), Password = Guid.NewGuid().ToString()};
			setRequest(req);
			return req;
		}
	}
}