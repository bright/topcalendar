using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ServerLib
{
    [DataContract]
    public class CalendarEntry
    {
        // tytul zadania
        [DataMember]
        public string Title { get; set; }

        // opis zadania
        [DataMember]
        public string Desc { get; set; }

        [DataMember]
        public DateTime DateTime
        {
            get; set;
        }

        /**
         * Nic ciekawego, na razie ustawia tylko tytul
         */
        public CalendarEntry() { }

        public CalendarEntry(string title)
        {
            Title = title;
            DateTime = DateTime.Now;
        }

        public CalendarEntry(string title, DateTime dateTime)
        {
            Title = title;
            DateTime = dateTime;
        }

        /**
         *  Dzień miesiąca  
         */
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
