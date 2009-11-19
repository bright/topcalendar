using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServerLib.Domain;

namespace ServerLib
{
    public interface IBaseCalendarEntryRepository
    {
        void Add(BaseCalendarEntry baseCalendarEntry);
        void Update(BaseCalendarEntry baseCalendarEntry);
        void Remove(BaseCalendarEntry baseCalendarEntry);
        IList<BaseCalendarEntry> FindAll();
        BaseCalendarEntry FindById(Guid id);
        IList<BaseCalendarEntry> FindByDay(int year, int month, int day);
        IList<BaseCalendarEntry> FindBetweenDates(DateTime from, DateTime to);
    }
}
