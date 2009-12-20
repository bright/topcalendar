using System;
using TopCalendar.Server.DataLayer.Tests;
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
			req.CurrentUser = New.User().WithLogin(req.UserCredentials.Login).WithPassword(req.UserCredentials.Password);
			setRequest(req);
			return req;
		}
	}
}