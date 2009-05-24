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
        /// <summary>
        ///  add entry to list
        /// </summary>
        /// <param name="e">entry to add</param>
        void Add(CalendarEntry e);

        /// <summary>
        ///  shoud be called when entry was edited
        /// </summary>
        /// <param name="e">entry that was edited</param>
        void EntryEdited(CalendarEntry e);


        /// <summary>
        /// remove entry form server
        /// </summary>
        /// <param name="e">entry to remove</param>
        void Remove(CalendarEntry e);

        List<CalendarEntry> GetTasksForDate(DateTime date);
    }
}
