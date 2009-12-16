using System;
using TopCalendar.Client.Connector.TopCalendarCommunicationService;

namespace TopCalendar.Client.Connector
{
	public abstract class ServiceClient
	{
		protected readonly ITopCalendarCommunicationService Service;
		protected readonly IClientContext ClientContext;

		protected ServiceClient(ITopCalendarCommunicationService topCalendarCommunicationService, IClientContext clientContext)
		{
			Service = topCalendarCommunicationService;
			ClientContext = clientContext;
		}

		protected TRequest Request<TRequest>(Action<TRequest> setThings)
			where TRequest : RequestWithCredentials
		{
			var request = Activator.CreateInstance<TRequest>();
			FillUserCredentials(request);
			setThings(request);			
			return request;
		}
		
		private void FillUserCredentials(RequestWithCredentials request)
		{			
			request.UserCredentials = ClientContext.UserCredentials;
		}
	}
}