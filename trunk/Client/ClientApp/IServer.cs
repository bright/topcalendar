using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientApp
{
    /// <summary>
    /// Interfejs, ktory musi implementowac klasa serwera.
    /// </summary>
    public interface IServer : IEnumerable<CalendarEntry>
    {
        void Add(CalendarEntry e);
    }
}
