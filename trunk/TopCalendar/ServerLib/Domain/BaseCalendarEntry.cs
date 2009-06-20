using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using ServerLib.Data;

namespace ServerLib.Domain
{
    [DataContract]
    public class BaseCalendarEntry
    {
        private static long lastId = 0;

        // identyfikator zadania
        [DataMember]
        public virtual long Id { get; set; }

        // tytul zadania
        [DataMember]
        public virtual string Title { get; set; }

        // opis zadania
        [DataMember]
        public virtual string Desc { get; set; }

        // czas zadania
        [DataMember]
        public virtual DateTime DateTime { get; set; }

        public BaseCalendarEntry()
        {
            Id = ++lastId; // ustawia id na pierwsze wolne
        }

        public BaseCalendarEntry(string title)
            : this()
        {
            Title = title;
        }

        public BaseCalendarEntry(string title, DateTime date)
            : this(title)
        {
            DateTime = date;
        }

        /// <summary>
        /// Operator rzutowania z obiektu eventu uzywanego w komunikacji na bazodanowy
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static implicit operator DbEntry(BaseCalendarEntry b)
        {
            // @TODO userID
            DbEntry d = DbEntry.CreateDbEntry(b.Id, b.Title, b.DateTime, 1);
            d.Description = b.Desc;
            return d;
        }

        public override bool Equals(object obj)
        {
            // If parameter is null return false.
            if (obj == null)
                return false;

            // If parameter cannot be cast to CalendarEntry return false.
            BaseCalendarEntry p = obj as BaseCalendarEntry;
            if (p == null)
                return false;

            // Return true if the fields match:
            return (this.Id == p.Id);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

     
    }
}