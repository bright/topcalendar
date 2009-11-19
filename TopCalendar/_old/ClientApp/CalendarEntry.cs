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

        public BaseCalendarEntry entry { get; private set; }

        public CalendarEntry()
        {
            entry = new BaseCalendarEntry();
        }

        public CalendarEntry(BaseCalendarEntry entry)
        {
            this.entry = entry;
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

        /// <summary>
        /// Operator rzutowania do zmiany BaseCalendarEntry na CalendatEntry
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static implicit operator CalendarEntry(BaseCalendarEntry e) {
            return new CalendarEntry(e);
        }

        // identyfikator zadania
        public Guid Id {
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
