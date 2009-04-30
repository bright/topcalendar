using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientApp
{
    public class CalendarEntry
    {
        public string Title { get; set; }

        public CalendarEntry() { }

        /**
         * Nic ciekawego, na razie ustawia tylko tytul
         */
        public CalendarEntry(string title)
        {
            Title = title;
        }
    }
}
