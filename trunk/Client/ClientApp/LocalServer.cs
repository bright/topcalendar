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
        public override int Count
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
        /// Z obiektu date odczytywane sa tylko pola day, month i year
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public override List<CalendarEntry> GetTasksForDate(DateTime date)
        {
            return GetTasksForDate(date.Day, date.Month, date.Year);
        }

        private List<CalendarEntry> GetTasksForDate(int day, int month, int year)
        {
            var result = (from item in this.data
                          where item.DateTime.Day == day
                              && item.DateTime.Month == month
                              && item.DateTime.Year == year
                          select item).ToList<CalendarEntry>();
            return result as List<CalendarEntry>;
        }

        public override IEnumerator<CalendarEntry> GetEnumerator()
        {
            return data.GetEnumerator();
        }
    }
}
