#region

using Ninject.Modules;
using TopCalendar.Client.Connector.MappingService;
using TopCalendar.Client.Connector.TopCalendarCommunicationService;

#endregion

namespace TopCalendar.Client.Connector
{
    public class ConnectorModule : NinjectModule
    {
        public override void Load()
        {
            // bindowanie proxy do komunikacji przez WS
            // binding ToConstant poniewaz Ninject nie potrafi sam utworzyc instancji 
            // klasy TopCalendarCommunicationServiceClient - chyba ze wzgledu na to,
            // ze App.config nie jest widziany przez ninject
            Bind<ITopCalendarCommunicationService>().ToConstant(new TopCalendarCommunicationServiceClient());

            Bind<IClientContext>().To<ClientContext>().InSingletonScope();

            Bind<IUserRegistrator>().To<UserRegistrator>();
            Bind<IUserAuthenticator>().To<UserAuthenticator>();
            Bind<ITaskRepository>().To<TasksRepository>().InSingletonScope();
            Bind<IMappingService>().To<PersistentMappingService>().InSingletonScope();
        }
    }
}