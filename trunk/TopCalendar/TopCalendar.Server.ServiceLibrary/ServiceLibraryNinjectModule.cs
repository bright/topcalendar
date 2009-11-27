#region

using Ninject.Modules;
using TopCalendar.Server.ServiceLibrary.ServiceContract;
using TopCalendar.Server.ServiceLibrary.ServiceImp;
using TopCalendar.Server.ServiceLibrary.ServiceLogic;

#endregion

namespace TopCalendar.Server.ServiceLibrary
{
    public class ServiceLibraryNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<UserRegistrationLogic>().ToSelf();
            Bind<ITopCalendarCommunicationService>().To<TopCalendarCommunicationServiceImpl>();
        }
    }
}