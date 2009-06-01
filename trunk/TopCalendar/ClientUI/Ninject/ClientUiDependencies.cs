using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClientApp;
using Ninject.Core;
using Ninject.Core.Behavior;

namespace ClientUI.Ninject
{

    public class ClientUiDependencies : StandardModule
    {
        public override void Load()
        {
            Bind<IDragAndDropService>().To<DragAndDropService>();
            Bind<IDragDestinationsHandler>().To<DragDestinationsHandler>().Using<SingletonBehavior>();
            Bind<IDayControlsService>().To<DayControlsService>().Using<SingletonBehavior>();
        }
    }
}
