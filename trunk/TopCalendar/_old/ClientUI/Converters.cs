using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;



namespace ClientUI
{
    [ValueConversion(typeof(DateTime), typeof(string))]
    class DateTimeToMonthName : IValueConverter
    {

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            DateTime dt = (DateTime)value;
            string monthName = culture.DateTimeFormat.MonthNames[dt.Month - 1];
            return monthName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    class DateTimeConverter : IValueConverter
    {
        public static readonly String DateTimePattern = "dd-MM-yyyy";

        public object Convert(object value,
                           Type targetType,
                           object parameter,
                           CultureInfo culture)
        {
            DateTime date = (DateTime)value;
            return date.ToString(DateTimePattern);
        }

        public object ConvertBack(object value,
                                  Type targetType,
                                  object parameter,
                                  CultureInfo culture)
        {
            string strValue = value.ToString();
            return DateTime.ParseExact(strValue, DateTimePattern, null);
            
        }
    }
}
