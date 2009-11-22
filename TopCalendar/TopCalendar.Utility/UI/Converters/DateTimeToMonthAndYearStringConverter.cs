using System;
using System.Globalization;
using System.Windows.Data;

namespace TopCalendar.Utility.UI.Converters
{
	public class DateTimeToMonthAndYearStringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var date = (DateTime) value;
			return HelperCollections.MonthNames[date.Month - 1] + " " + date.Year;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}