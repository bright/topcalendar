using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ServerLib
{
    [DataContract]
    public class BaseCalendarEntry
    {
        private static long lastId = 0;

        // identyfikator zadania
        [DataMember]
        public long Id { get; set; }

        // tytul zadania
        [DataMember]
        public string Title { get; set; }

        // opis zadania
        [DataMember]
        public string Desc { get; set; }

        // czas zadania
        [DataMember]
        public DateTime DateTime { get; set; }

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

        // dzień miesiąca - po co? 
        public int Day
        {
            get { return DateTime.Day; }
        }

        public int Hour
        {
            get { return DateTime.Hour; }
        }

        public int Minute
        {
            get { return DateTime.Minute; }
        }

        public int Year
        {
            get { return DateTime.Year; }
        }

        public int Month
        {
            get { return DateTime.Month; }
        }
    }
}
