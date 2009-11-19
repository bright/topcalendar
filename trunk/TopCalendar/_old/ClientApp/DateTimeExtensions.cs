using System;


namespace ClientApp.DateTimeExtensions {

    public static class DateTimeExtensions {

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

        public static DateTime WeekStart(this DateTime dt)
        {
            DateTime result = new DateTime(dt.Year,dt.Month,dt.Day);
            while (result.DayOfWeek != DayOfWeek.Monday)
                result = result.AddDays(-1);
            return result;
        }

        public static DateTime WeekEnd(this DateTime dt)
        {
            DateTime result = new DateTime(dt.Year,dt.Month,dt.Day,23,59,59,99);
            while(result.DayOfWeek != DayOfWeek.Sunday)
                result = result.AddDays(1);
            return result;
        }
    
    }
}