using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientApp
{
    public class CalendarEntry
    {
        public string Title { get; set; }

        public CalendarEntry() { }

        /**
         * Nic ciekawego, na razie ustawia tylko tytul
         */
        public CalendarEntry(string title)
        {
            Title = title;
            dateTime = DateTime.Now;
        }
        
        public CalendarEntry(string title, DateTime dateTime)
        {
            Title = title;
            this.dateTime = dateTime;
        }

        private DateTime dateTime;

        /**
         *  Dzień miesiąca  
         */
        public int Day
        {
            get { return dateTime.Day; }
        }

        public int Hour
        {
            get { return dateTime.Hour; }
        }

        public int Minute
        {
            get { return dateTime.Minute; }
        }
        public int Year
        {
            get { return dateTime.Year; }
        }
        public int Month
        {
            get { return dateTime.Month; }
        }
    }
}
