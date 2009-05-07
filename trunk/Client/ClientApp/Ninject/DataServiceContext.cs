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
    public class DataServiceContext : IDataServiceBroker
    {
        // kontekst musi byc singletonem - jedyny prawdziwy singleton w aplikacji :)
        private static readonly DataServiceContext current = new DataServiceContext();
        // glowny modul Ninjecta
        private IKernel kernel;

        public static DataServiceContext Current
        {
            get { return current; }
        }

        public DataServiceContext()
        {
            kernel = new StandardKernel(new DataServiceModule());
        }

        #region IDataServiceBroker Members
       
        public IServer LocalServer
        {
            get { return kernel.Get<IServer>(); }
        }

        public NewEntryCreator NewEntryCreator
        {
            get { return kernel.Get<NewEntryCreator>(); }
        }

        #endregion
    }
}
