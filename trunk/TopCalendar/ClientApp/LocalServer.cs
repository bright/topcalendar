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
        private List<BaseCalendarEntry> data = new List<BaseCalendarEntry>();

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
        public override void Add(BaseCalendarEntry e)
        {
            data.Add(e);
            FireEntriesListChangedEvent(null);
        }

        public override void Remove(BaseCalendarEntry e)
        {
            data.Remove(e);
            FireEntriesListChangedEvent(null);
        }

        public override void EntryEdited(BaseCalendarEntry e)
        {
            FireEntriesListChangedEvent(null);
        }

        /// <summary>
        /// Pobranie listy zadan dla podanego dnia 
        /// </summary>
        /// <returns></returns>
        public override List<BaseCalendarEntry> GetTasksForDate(int day, int month, int year)
        {
            var result = (from item in this.data
                          where item.DateTime.Day == day
                              && item.DateTime.Month == month
                              && item.DateTime.Year == year
                          select item).ToList<BaseCalendarEntry>();
            return result as List<BaseCalendarEntry>;
        }

        public override IEnumerable<BaseCalendarEntry> Enumerate()
        {
            return data.AsReadOnly();
        }
    }
}
