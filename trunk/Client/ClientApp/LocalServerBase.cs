using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientApp
{
    public abstract class LocalServerBase : IServer, ClientApp.ILocalServerBase
    {

        public delegate void EntriesListDelegate(object sender, EventArgs e);

        public event EntriesListDelegate EntriesListChanged;
        /// <summary>
        /// 
        /// </summary>
        /// <returns>enumerator to entries list</returns>
        public abstract IEnumerator<CalendarEntry> GetEnumerator();


        public abstract void Add(CalendarEntry e);

        public abstract void EntryEdited(CalendarEntry e);

        public abstract void Remove(CalendarEntry e);

        public abstract List<CalendarEntry> GetTasksForDate(DateTime date);

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        protected void FireEntriesListChangedEvent (EventArgs e)
        {
            if (EntriesListChanged != null)

                EntriesListChanged(this,e);
        }
    }
}
