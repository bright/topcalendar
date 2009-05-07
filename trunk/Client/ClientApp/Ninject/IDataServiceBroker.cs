using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientApp.Ninject
{
    /// <summary>
    /// Interfejs udostepniajacy metody do uzyskiwania dostepu do
    /// obiektow roznych klas 
    /// Wiecej szczegolow w klasie, ktora implementuje ten interfejs
    /// </summary>
    public interface IDataServiceBroker
    {
        IServer LocalServer { get; }
        NewEntryCreator NewEntryCreator { get; }
    }

}
