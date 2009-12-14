namespace TopCalendar.Client.Connector
{
    public interface IUserRegistrator
    {
        bool IsLoginFree(string login);
        bool Register(string login, string password);
    }
}