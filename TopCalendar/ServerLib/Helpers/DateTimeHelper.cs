using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServerLib.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTime StartOfDay(int year, int month, int day)
        {
            return new DateTime(year, month, day, 0, 0, 0, 0);
        }

        public static DateTime EndOfDay(int year, int month, int day)
        {
            return new DateTime(year, month, day, 23, 59, 59, 999);
        }
    }
}
