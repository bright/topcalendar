using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClientApp.RemoteServerRef;

namespace ClientApp
{
    public class LocalServer : ServerBase
    {
        private List<CalendarEntry> data = new List<CalendarEntry>();

        /**
         * Daje dostep tylko do odczytu do licznika listy
         */
        public override int get_Count()
        {
            return data.Count;
        }

        /**
         * Dodaje wydarzenie do listy
         */
        public override void Add(CalendarEntry e)
        {
            data.Add(e);
            FireEntriesListChangedEvent(null);
        }

        public override void Remove(CalendarEntry e)
        {
            data.Remove(e);
            FireEntriesListChangedEvent(null);
        }

        public override void EntryEdited(CalendarEntry e)
        {
            FireEntriesListChangedEvent(null);
        }

        /// <summary>
        /// Pobranie listy zadan dla podanego dnia 
        /// </summary>
        /// <returns></returns>
        public override List<CalendarEntry> GetTasksForDate(int day, int month, int year)
        {
            var result = (from item in this.data
                          where item.DateTime.Day == day
                              && item.DateTime.Month == month
                              && item.DateTime.Year == year
                          select item).ToList<CalendarEntry>();
            return result as List<CalendarEntry>;
        }

        public override IEnumerable<CalendarEntry> Enumerate()
        {
            return data.AsReadOnly();
        }
    }
}
