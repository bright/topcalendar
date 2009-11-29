using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TopCalendar.Utility.UI.Converters
{
	[ValueConversion(typeof(string),typeof(Visibility))]
	public class BoolToVisibilityConverter : IValueConverter
	{


		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			bool isVisible = (bool) value;
			if (isVisible)
			{
				return Visibility.Visible;
			}
			else
			{
				return Visibility.Collapsed;	
			}						
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}