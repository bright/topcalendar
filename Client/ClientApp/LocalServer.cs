using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientApp
{
    public class LocalServer : LocalServerBase
    {

        private List<CalendarEntry> data = new List<CalendarEntry>();

        /**
         * Daje dostep tylko do odczytu do licznika listy
         */
        public int Count
        {
            get
            {
                return data.Count;
            }
        }

        /**
         * Dodaje wydarzenie do listy
         */
        public override void Add(CalendarEntry e)
        {
            data.Add(e);
            FireEntriesListChangedEvent(null);
        }

        public override IEnumerator<CalendarEntry> GetEnumerator()
        {
            return data.GetEnumerator();
        }
    }
}
