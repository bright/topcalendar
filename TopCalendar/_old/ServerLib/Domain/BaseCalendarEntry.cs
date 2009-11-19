using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ServerLib.Domain
{
    [DataContract]
    public class BaseCalendarEntry
    {

        // identyfikator zadania
        [DataMember]
        public virtual Guid Id { get; set; }

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

        public BaseCalendarEntry(string title, string desc, DateTime dateTime)
        {
            Title = title;
            Desc = desc;
            DateTime = dateTime;
        }

        public override bool Equals(object obj)
        {
            // If parameter is null return false.
            if (obj == null)
                return false;

            // If parameter cannot be cast to CalendarEntry return false.
            var p = obj as BaseCalendarEntry;
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