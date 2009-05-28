using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ServerLib
{
    // w serwerze nie potrzebujemy konstruktorow ani niczego do obslugi danych -
    // tylko elementy niezbedne do definicji kontraktu danych WCF
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
    }
}
