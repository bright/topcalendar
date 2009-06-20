using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;
using ServerLib.Domain;

namespace ServerLib.Data
{
    /// <summary>
    /// Klasa rozszerzajaca obiekt bazodanowy o operator rzutowania
    /// na obiekt komunikacji WCF
    /// </summary>
    public partial class DbEntry : EntityObject
    {
        public static implicit operator BaseCalendarEntry(DbEntry d)
        {
            BaseCalendarEntry b = new BaseCalendarEntry();
            b.Id = d.Id;
            b.Title = d.Title;
            b.Desc = d.Description;
            b.DateTime = d.DateFrom;

            return b;
        }
    }
}
