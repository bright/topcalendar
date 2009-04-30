using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientApp
{
    public class LocalServer : IEnumerable<CalendarEntry>
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
        public void Add(CalendarEntry e)
        {
            data.Add(e);
        }

        /**
         * Implementujemy interfejsik zeby mozna bylo pytac nasz "serwer" przez LINQ,
         * jednoczesnie nie odkrywajac na zewnatrz calej listy
         */
        #region IEnumerable<CalendarEntry> Members

        IEnumerator<CalendarEntry> IEnumerable<CalendarEntry>.GetEnumerator()
        {
            return data.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return data.GetEnumerator();
        }

        #endregion
    }
}
