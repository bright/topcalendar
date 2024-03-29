using Ninject.Modules;
using TopCalendar.Client.Connector.TopCalendarCommunicationService;

namespace TopCalendar.Client.Connector
{
    public class UserRegistratorNinjectModule : Module
    {

        public override void Load()
        {
            
            // bindowanie proxy do komunikacji przez WS
            // binding ToConstant poniewaz Ninject nie potrafi sam utworzyc instancji 
            // klasy TopCalendarCommunicationServiceClient - chyba ze wzgledu na to,
            // ze App.config nie jest widziany przez ninject
            Bind<ITopCalendarCommunicationService>().ToConstant(new TopCalendarCommunicationServiceClient());

            Bind<IUserRegistrator>().To<UserRegistrator>();

        }
    }
}