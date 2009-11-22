using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;

namespace TopCalendar.Utility.UI.Converters
{
	public class ValidationErrorToStringConverter:  IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var errors = value as ReadOnlyObservableCollection<ValidationError>;

			if (errors == null)
			{
				return string.Empty;
			}

			return string.Join("\n", (from e in errors
									  select e.ErrorContent as string).ToArray());
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}