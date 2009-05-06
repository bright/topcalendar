using System;


namespace ClientApp.DateTimeExtensions {

    public static class DateTimeExtesions {

        public static DateTime MonthStart(this DateTime dt) {
            return new DateTime(dt.Year, dt.Month, 1);
        }

        public static DateTime NextMonth(this DateTime dt) {
            int month = dt.Month;
            int year = dt.Year;
            if (month == 12)
            {
                month = 1;
                ++year;
            }
            else
                ++month;
            return new DateTime(year, month, 1);
        }


        public static DateTime PrevMonth(this DateTime dt)
        {
            int month = dt.Month;
            int year = dt.Year;
            if (month == 1) {
                month = 12;
                --year;
            } else
                --month;
            return new DateTime(year, month, 1);
        }
    }
}