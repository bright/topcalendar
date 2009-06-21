using System;
using System.Collections.Generic;
using System.ServiceModel;
using ServerLib.Domain;
using ServerLib.Repositories;

namespace ServerLib
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class CalendarServer : IServer
    {
        // obiekt zapewniajacy obsluge warstwy bazodanowej
        //private DatabaseEntities dao = new DatabaseEntities();

        private readonly IBaseCalendarEntryRepository _repository = new BaseCalendarEntryRepository();

        #region IServer Members

        public event EventHandler<EventArgs> EntriesListChanged;

        /// <summary>
        /// Daje dostep tylko do odczytu do licznika listy
        /// </summary>
        public int Count
        {
            get { return 17; }
        }

        /// <summary>
        /// Dodaje wydarzenie do listy
        /// </summary>
        /// <param name="e"></param>
        public void Add(BaseCalendarEntry e)
        {
            _repository.Add(e);

            FireEntriesListChangedEvent(null);
        }

        public void Edit(BaseCalendarEntry e)
        {
            _repository.Update(e);

            FireEntriesListChangedEvent(null);
        }

        public void Remove(BaseCalendarEntry e)
        {
            _repository.Remove(e);

            FireEntriesListChangedEvent(null);
        }

        public void EntryEdited(BaseCalendarEntry e)
        {
            _repository.Update(e);
            FireEntriesListChangedEvent(null);
        }


        /// <summary>
        /// Pobranie listy zadan dla podanego dnia 
        /// </summary>
        /// <returns></returns>
        public IList<BaseCalendarEntry> GetTasksForDate(int day, int month, int year)
        {
            return _repository.FindByDay(year, month, day);
        }

        public IList<BaseCalendarEntry> GetTasksBeetweenDates(DateTime from, DateTime to)
        {
            return _repository.FindBetweenDates(from, to);
        }

        public IEnumerable<BaseCalendarEntry> Enumerate()
        {
            IList<BaseCalendarEntry> tmpList = _repository.FindAll();

            // niestety listy nie umieja sie ladnie przekonwertowac :(
            var retList = new List<BaseCalendarEntry>();
            retList.AddRange(tmpList);            

            return retList.AsReadOnly();
        }

        #endregion

        protected void FireEntriesListChangedEvent(EventArgs e)
        {
            if (EntriesListChanged != null)
                EntriesListChanged(this, e);
        }

        public IList<BaseCalendarEntry> GetTasksForDate(DateTime date)
        {
            return GetTasksForDate(date.Day, date.Month, date.Year);
        }
    }
}