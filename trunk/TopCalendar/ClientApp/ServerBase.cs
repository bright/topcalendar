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

        public abstract List<BaseCalendarEntry> Enumerate();

        public abstract void Add(BaseCalendarEntry e);

        public abstract void EntryEdited(BaseCalendarEntry e);

        public abstract void Remove(BaseCalendarEntry e);

        public abstract List<BaseCalendarEntry> GetTasksForDate(int day, int month, int year);

        public abstract int get_Count();

        protected void FireEntriesListChangedEvent (EventArgs e)
        {
            if (EntriesListChanged != null)
                EntriesListChanged(this, e);
        }
    }
}
