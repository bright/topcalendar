using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Core;
using ClientApp.RemoteServerRef;

namespace ClientApp
{
    /// <summary>
    /// Klasa obslugujaca proces dodawania nowego zadania.
    /// </summary>
    public class NewEntryCreator
    {
        private IServer server;

        public NewEntryCreator(IServer server)
        {
            this.server = server;
        }

        public CalendarEntry CalendarEntry { get; set; }

        public void Save()
        {
            if (CalendarEntry == null)
            {
                throw new InvalidOperationException("CalendarEntry is null");
            }

            server.Add(CalendarEntry);
        }

    }
}
