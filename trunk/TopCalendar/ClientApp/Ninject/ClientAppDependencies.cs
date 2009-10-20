using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClientApp.RemoteServerRef;
using Ninject.Modules;

namespace ClientApp.Ninject
{
    /// <summary>
    /// Definiuje reguly bindowania typow przez Ninject.
    /// </summary>
    public class ClientAppDependencies : Module
    {
        public override void Load()
        {
            // LocalServer jest teraz singletonem (ale w przyszlosci nie musi)
        	Bind<IServer>().To<RemoteServer>().InSingletonScope();
        	Bind<ServerBase>().To<RemoteServer>().InSingletonScope();
            
            // Klasa NewEntryCreator bindowana do samej siebie
            Bind<NewEntryCreator>().ToSelf();
        }

    }
}
