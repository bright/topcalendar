using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace ServerLib
{
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerSession)]
    public class CalendarServer : IServer
    {
        private List<BaseCalendarEntry> data = new List<BaseCalendarEntry>();

        public event EventHandler<EventArgs> EntriesListChanged;

        /*IEnumerator IEnumerable.GetEnumerator()
        {
            return data.GetEnumerator();
        }*/

        protected void FireEntriesListChangedEvent(EventArgs e)
        {
            if (EntriesListChanged != null)
                EntriesListChanged(this, e);
        }

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
        public void Add(BaseCalendarEntry e)
        {
            data.Add(e);
            FireEntriesListChangedEvent(null);
        }

        public void Remove(BaseCalendarEntry e)
        {
            data.Remove(e);
            FireEntriesListChangedEvent(null);
        }

        public void EntryEdited(BaseCalendarEntry e)
        {
            FireEntriesListChangedEvent(null);
        }

      
        /// <summary>
        /// Pobranie listy zadan dla podanego dnia 
        /// </summary>
        /// <returns></returns>
        public List<BaseCalendarEntry> GetTasksForDate(int day, int month, int year)
        {
            var result = (from item in this.data
                          where item.DateTime.Day == day
                              && item.DateTime.Month == month
                              && item.DateTime.Year == year
                          select item).ToList<BaseCalendarEntry>();
            return result as List<BaseCalendarEntry>;
        }

        public List<BaseCalendarEntry> GetTasksForDate(DateTime date)
        {
            return GetTasksForDate(date.Day, date.Month, date.Year);
        }

        public IEnumerable<BaseCalendarEntry> Enumerate()
        {
            return data.AsReadOnly();
        }
    }
}
