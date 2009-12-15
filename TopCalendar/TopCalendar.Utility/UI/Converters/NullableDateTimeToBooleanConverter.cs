using System;
using System.Globalization;
using System.Windows.Data;

namespace TopCalendar.Utility.UI.Converters
{
	public class NullableDateTimeToBooleanConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var date = value as DateTime?;
			return date != null ? date.HasValue : false;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}