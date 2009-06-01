using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClientApp.RemoteServerRef;

namespace ClientApp
{
    /**
     * Proxy dla zdalnego serwera
     */
    public class RemoteServer : ServerBase
    {
        private IServer server = new ServerClient();

        /**
         * Daje dostep tylko do odczytu do licznika listy
         */
        public override int get_Count()
        {
            return server.get_Count();
        }

        /**
         * Dodaje wydarzenie do listy
         */
        public override void Add(CalendarEntry e)
        {
            server.Add(e);
        }

        public override void Remove(CalendarEntry e)
        {
            server.Remove(e);
        }

        public override void EntryEdited(CalendarEntry e)
        {
            server.EntryEdited(e);
        }

        /// <summary>
        /// Pobranie listy zadan dla podanego dnia 
        /// </summary>
        /// <returns></returns>
        public override List<CalendarEntry> GetTasksForDate(int day, int month, int year)
        {
            return server.GetTasksForDate(day, month, year);
        }

        public override IEnumerable<CalendarEntry> Enumerate()
        {
            return server.Enumerate();
        }
    }
}
