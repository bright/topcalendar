using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace ServerLib
{
    /// <summary>
    /// Interfejs, ktory musi implementowac klasa serwera.
    /// </summary>
    [ServiceContract]
    public interface IServer //: IEnumerable<CalendarEntry>
    {
        /// <summary>
        ///  add entry to list
        /// </summary>
        /// <param name="e">entry to add</param>
        [OperationContract]
        void Add(CalendarEntry e);

        /// <summary>
        ///  shoud be called when entry was edited
        /// </summary>
        /// <param name="e">entry that was edited</param>
        [OperationContract]
        void EntryEdited(CalendarEntry e);

        /// <summary>
        /// remove entry form server
        /// </summary>
        /// <param name="e">entry to remove</param>
        [OperationContract]
        void Remove(CalendarEntry e);

        [OperationContract]
        List<CalendarEntry> GetTasksForDate(int day, int month, int year);

        int Count
        {
            [OperationContract]
            get;
        }

        [OperationContract]
        IEnumerable<CalendarEntry> Enumerate();

        /*
         * TODO: eventow nie da sie udostepniac przez WCF
         * trzeba je przerobic na interfejs subskrypcji i powiadomien (one-way)
         */
        event EventHandler<EventArgs> EntriesListChanged;
    }
}
