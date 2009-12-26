using System;
using System.Globalization;
using System.Windows.Data;

namespace TopCalendar.Utility.UI.Converters
{
	public class DateTimeToHourStringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var dt = (DateTime) value;
			return dt.ToShortTimeString();
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}