using System;
using ClientApp.RemoteServerRef;

namespace ClientApp
{
    /// <summary>
    /// Klasa obslugujaca proces dodawania nowego zadania.
    /// </summary>
    public class NewEntryCreator
    {
        private readonly IServer _server;


        public NewEntryCreator(IServer server)
        {
            this._server = server;
        }

        public CalendarEntry CalendarEntry { get; set; }

        public void Save()
        {
            if (CalendarEntry == null)
            {
                throw new InvalidOperationException("CalendarEntry is null");
            }

            if (CalendarEntry.Id == Guid.Empty)
            {
                _server.Add(CalendarEntry);
            } else
            {
                _server.EntryEdited(CalendarEntry);
            }


        }
    }
}