using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Core;

namespace ClientApp.Ninject
{
    /// <summary>
    /// Klasa przykrywajaca dzialanie Ninjecta.
    /// Dzieki temu w calej aplikacji tworzenie obiektow odbywa sie
    /// za pomoca interfejsu niezaleznego od Ninjecta. 
    /// Mozna dzieki temu np. dowolnie zmieniac framework IoC.
    /// </summary>
    public static class Factory 
    {
        // glowny modul Ninjecta
        private static IKernel kernel = new StandardKernel(new ClientAppDependencies());

        public static T Resolve<T>()
        {           
            return kernel.Get<T>();
        }

        public static void Load(IModule module)
        {
            kernel.Load(module);
        }

     

    }
}
