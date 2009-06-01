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
        private List<CalendarEntry> data = new List<CalendarEntry>();

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
        public void Add(CalendarEntry e)
        {
            data.Add(e);
            FireEntriesListChangedEvent(null);
        }

        public void Remove(CalendarEntry e)
        {
            data.Remove(e);
            FireEntriesListChangedEvent(null);
        }

        public void EntryEdited(CalendarEntry e)
        {
            FireEntriesListChangedEvent(null);
        }

      
        /// <summary>
        /// Pobranie listy zadan dla podanego dnia 
        /// </summary>
        /// <returns></returns>
        public List<CalendarEntry> GetTasksForDate(int day, int month, int year)
        {
            var result = (from item in this.data
                          where item.DateTime.Day == day
                              && item.DateTime.Month == month
                              && item.DateTime.Year == year
                          select item).ToList<CalendarEntry>();
            return result as List<CalendarEntry>;
        }

        public List<CalendarEntry> GetTasksForDate(DateTime date)
        {
            return GetTasksForDate(date.Day, date.Month, date.Year);
        }

        public IEnumerable<CalendarEntry> Enumerate()
        {
            return data.AsReadOnly();
        }
    }
}
