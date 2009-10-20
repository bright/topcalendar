using Ninject.Modules;

namespace ClientUI.Ninject
{

    public class ClientUiDependencies : Module
    {
        public override void Load()
        {
            Bind<IDragAndDropService>().To<DragAndDropService>();
            Bind<IDragDestinationsHandler>().To<DragDestinationsHandler>().InSingletonScope();
            Bind<IDayControlsService>().To<DayControlsService>().InSingletonScope();
        }
    }
}
