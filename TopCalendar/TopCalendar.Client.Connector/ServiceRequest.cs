using System;
using Microsoft.Practices.ServiceLocation;
using TopCalendar.Client.Connector.TopCalendarCommunicationService;

namespace TopCalendar.Client.Connector
{
	public class ServiceRequest
	{
		public static TRequest Of<TRequest>(Action<TRequest> setThings)
			where TRequest : RequestWithCredentials
		{
			var request = Activator.CreateInstance<TRequest>();
			setThings(request);
			FillUserCredentials(request);
			return request;
		}

		private static IClientContext context;
		private static void FillUserCredentials(RequestWithCredentials request)
		{
			if (context == null)
				context = ServiceLocator.Current.GetInstance<IClientContext>();
			request.UserCredentials = context.UserCredentials;
		}
	}
}