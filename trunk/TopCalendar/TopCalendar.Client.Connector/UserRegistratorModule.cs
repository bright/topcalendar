using Ninject.Modules;

namespace TopCalendar.Client.Connector
{
    public class UserRegistratorModule : Module
    {
        public override void Load()
        {
            Bind<IUserRegistrator>().To<UserRegistrator>();
        }
    }
}