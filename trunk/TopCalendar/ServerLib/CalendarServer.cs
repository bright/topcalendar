using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using ServerLib.Data;

namespace ServerLib
{
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerSession)]
    public class CalendarServer : IServer
    {
        // obiekt zapewniajacy obsluge warstwy bazodanowej
        private DatabaseEntities dao = new DatabaseEntities();

        public event EventHandler<EventArgs> EntriesListChanged;

        protected void FireEntriesListChangedEvent(EventArgs e)
        {
            if (EntriesListChanged != null)
                EntriesListChanged(this, e);
        }

        /// <summary>
        /// Daje dostep tylko do odczytu do licznika listy
        /// </summary>
        public int Count
        {
            get
            {
                return dao.DbEntrySet.Count<DbEntry>();
            }
        }

        /// <summary>
        /// Dodaje wydarzenie do listy
        /// </summary>
        /// <param name="e"></param>
        public void Add(BaseCalendarEntry e)
        {
            // rzutowanie odbywa sie automatycznie za pomoca operatora
            // rzutowania zdefiniowanego w BaseCalendarEntry
            dao.AddToDbEntrySet(e);
            dao.SaveChanges();

            FireEntriesListChangedEvent(null);
        }

        public void Remove(BaseCalendarEntry e)
        {
            DbEntry entryToDelete = (from entry in dao.DbEntrySet
                  where entry.Id == e.Id
                  select entry).First();

            dao.DeleteObject(entryToDelete);
            dao.SaveChanges();

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
            var result = (from item in dao.DbEntrySet
                          where item.DateFrom.Day == day
                              && item.DateFrom.Month == month
                              && item.DateFrom.Year == year
                          select item).ToList<DbEntry>();

            List<DbEntry> tmpList = result as List<DbEntry>;

            // niestety listy nie umieja sie ladnie przekonwertowac :(
            List<BaseCalendarEntry> retList = new List<BaseCalendarEntry>();
            foreach (BaseCalendarEntry e in tmpList)
                retList.Add(e);

            return retList;
        }

        public List<BaseCalendarEntry> GetTasksForDate(DateTime date)
        {
            return GetTasksForDate(date.Day, date.Month, date.Year);
        }

        public IEnumerable<BaseCalendarEntry> Enumerate()
        {
            List<DbEntry> tmpList = dao.DbEntrySet.ToList<DbEntry>();

            // niestety listy nie umieja sie ladnie przekonwertowac :(
            List<BaseCalendarEntry> retList = new List<BaseCalendarEntry>();
            foreach (BaseCalendarEntry e in tmpList)
                retList.Add(e);

            return retList.AsReadOnly();
        }
    }
}
