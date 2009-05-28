using System;
namespace ServerLib
{
    interface IRemoteServerBase
    {
        void Add(CalendarEntry e);
        event RemoteServerBase.EntriesListDelegate EntriesListChanged;
        System.Collections.Generic.IEnumerator<CalendarEntry> GetEnumerator();
    }
}
