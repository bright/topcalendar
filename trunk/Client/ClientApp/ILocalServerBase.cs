using System;
namespace ClientApp
{
    interface ILocalServerBase
    {
        void Add(CalendarEntry e);
        event LocalServerBase.EntriesListDelegate EntriesListChanged;
        System.Collections.Generic.IEnumerator<CalendarEntry> GetEnumerator();
    }
}
