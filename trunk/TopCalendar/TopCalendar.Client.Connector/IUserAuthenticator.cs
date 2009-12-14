namespace TopCalendar.Client.Connector
{
    public interface IUserAuthenticator
    {
        bool Login(string login, string password);
    }
}