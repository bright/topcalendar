using TopCalendar.Client.Connector.TopCalendarCommunicationService;

namespace TopCalendar.Client.Connector
{
    public interface IClientContext
    {
        UserCredentials UserCredentials { get; }

        void OnUserLogged(UserCredentials userCredentials);
    }
}