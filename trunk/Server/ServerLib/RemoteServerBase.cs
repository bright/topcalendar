using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServerLib
{
    public abstract class RemoteServerBase : IServer
    {
        public event EventHandler<EventArgs> EntriesListChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <returns>enumerator to entries list</returns>
        public abstract IEnumerator<CalendarEntry> GetEnumerator();

        public abstract void Add(CalendarEntry e);

        public abstract void EntryEdited(CalendarEntry e);

        public abstract void Remove(CalendarEntry e);

        public abstract List<CalendarEntry> GetTasksForDate(DateTime date);

        public abstract int Count { get; }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        protected void FireEntriesListChangedEvent (EventArgs e)
        {
            if (EntriesListChanged != null)
                EntriesListChanged(this, e);
        }
    }
}
