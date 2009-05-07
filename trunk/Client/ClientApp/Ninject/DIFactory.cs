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
    public static class DIFactory 
    {
        // glowny modul Ninjecta
        private static IKernel kernel;

        public static T Resolve<T>()
        {

            if (kernel == null)
            {
                CreateKernel();
            }
            return kernel.Get<T>();

        }

        private static void CreateKernel()
        {  
            IModule module = new DIModule();
            kernel = new StandardKernel(module);

        }

    }
}
