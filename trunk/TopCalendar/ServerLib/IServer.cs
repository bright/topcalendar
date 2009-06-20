using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using ServerLib.Domain;

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
        void Add(BaseCalendarEntry e);

        /// <summary>
        ///  shoud be called when entry was edited
        /// </summary>
        /// <param name="e">entry that was edited</param>
        [OperationContract]
        void EntryEdited(BaseCalendarEntry e);

        /// <summary>
        /// remove entry form server
        /// </summary>
        /// <param name="e">entry to remove</param>
        [OperationContract]
        void Remove(BaseCalendarEntry e);

        [OperationContract]
        IList<BaseCalendarEntry> GetTasksForDate(int day, int month, int year);

        int Count
        {
            [OperationContract]
            get;
        }

        [OperationContract]        
        IEnumerable<BaseCalendarEntry> Enumerate();

        /*
         * TODO: eventow nie da sie udostepniac przez WCF
         * trzeba je przerobic na interfejs subskrypcji i powiadomien (one-way)
         */
        event EventHandler<EventArgs> EntriesListChanged;
    }
}
