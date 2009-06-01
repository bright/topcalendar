using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClientApp.RemoteServerRef;

namespace ClientApp
{
    public abstract class ServerBase : IServer
    {
        public event EventHandler<EventArgs> EntriesListChanged;

        public abstract IEnumerable<CalendarEntry> Enumerate();

        public abstract void Add(CalendarEntry e);

        public abstract void EntryEdited(CalendarEntry e);

        public abstract void Remove(CalendarEntry e);

        public abstract List<CalendarEntry> GetTasksForDate(int day, int month, int year);

        public abstract int get_Count();

        protected void FireEntriesListChangedEvent (EventArgs e)
        {
            if (EntriesListChanged != null)
                EntriesListChanged(this, e);
        }
    }
}
