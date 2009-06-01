using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClientApp.RemoteServerRef;

namespace ClientApp
{
    /// <summary>
    /// klasa - wrapper dla serwerowego BaseCalendarEntry
    /// </summary>
    public class CalendarEntry
    {
        private static long lastId = 0;

        public BaseCalendarEntry entry { get; private set; }

        public CalendarEntry()
        {
            entry = new BaseCalendarEntry();
            Id = ++lastId; // pierwszy wolny w kliencie - tymczasowo
        }

        /// <summary>
        /// Operator rzutowania na typ do komunikacji via WCF
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static implicit operator BaseCalendarEntry(CalendarEntry e)
        {
            return e.entry;
        }

        // identyfikator zadania
        public long Id {
            get { return entry.Id; }
            private set { entry.Id = value; }
        }

        // tytul zadania
        public string Title {
            get { return entry.Title; }
            set { entry.Title = value; }
        }

        // opis zadania
        public string Desc {
            get { return entry.Desc; }
            set { entry.Desc = value; }
        }

        // czas zadania
        public DateTime DateTime {
            get { return entry.DateTime; }
            set { entry.DateTime = value; }
        }
    }
}
