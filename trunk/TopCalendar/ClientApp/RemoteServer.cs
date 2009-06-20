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


        public RemoteServer() {
            ((ServerClient)server).ClientCredentials.UserName.UserName = "test";
            ((ServerClient)server).ClientCredentials.UserName.Password = "test";
        }


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
        public override void Add(BaseCalendarEntry e)
        {
            server.Add(e);
            FireEntriesListChangedEvent(null);
        }

        public override void Edit(BaseCalendarEntry e)
        {
            server.Edit(e);
        }

        public override void Remove(BaseCalendarEntry e)
        {
            server.Remove((BaseCalendarEntry)e);
            FireEntriesListChangedEvent(null);
        }

        public override void EntryEdited(BaseCalendarEntry e)
        {
            server.EntryEdited((BaseCalendarEntry)e);
            FireEntriesListChangedEvent(null);
        }

        /// <summary>
        /// Pobranie listy zadan dla podanego dnia 
        /// </summary>
        /// <returns></returns>
        public override List<BaseCalendarEntry> GetTasksForDate(int day, int month, int year)
        {
            return server.GetTasksForDate(day, month, year);
        }

        public override List<BaseCalendarEntry> Enumerate()
        {
            return server.Enumerate();
        }
    }
}
