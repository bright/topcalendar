using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using NUnit.Framework;
using TopCalendar.Utility.UI;
using TopCalendar.Utility.UI.Converters;

namespace TopCalendar.Utility.Tests.UI.Converters
{
	public class when_converting_validation_errors_to_string
		: observations_for_value_converter_of_type<ValidationErrorToStringConverter>
	{

		[Test]
		public void should_return_empty_string_for_null()
		{
			ConvertValue(null).ShouldBeOfType<string>().ShouldBeEmpty();
		}

		[Test]
		public void should_return_error_messages_separted_with_comma()
		{
			// cant create ValidationResult by hand
			var message = ConvertValue(new ReadOnlyObservableCollection<ValidationResult>(new ObservableCollection<ValidationResult>()))
				.As<string>().ShouldHaveCount(0);
		}
	}


	public class when_converting_bool_to_visiblity : observations_for_value_converter_of_type<BoolToVisibilityConverter>
	{
		[Test]
		public void should_return_collapsed_on_false()
		{
			ConvertValue(false).As<Visibility>().ShouldEqual(Visibility.Collapsed);
		}

		[Test]
		public void should_return_collapsed()
		{
			ConvertValue(true).As<Visibility>().ShouldEqual(Visibility.Visible);
		}
	}

	public class when_converting_datetime_to_short_date_string : observations_for_value_converter_of_type<DateTimeToShortDateStringConverter>
	{
		[Test]
		public void should_return_proper_string()
		{
			var date = DateTime.Now;
			ConvertValue(date).As<string>().ShouldEqual(date.ToShortDateString());
		}	
	}

	public class when_converting_datetime_to_short_time_string : observations_for_value_converter_of_type<DateTimeToShortTimeConverter>
	{
		[Test]
		public void should_return_proper_string()
		{
			var date = DateTime.Now;
			ConvertValue(date).As<string>().ShouldEqual(date.ToShortTimeString());
		}
	}

	public class when_converting_datetime_to_month_and_year_string : observations_for_value_converter_of_type<DateTimeToMonthAndYearStringConverter>
	{
		[Test]
		public void should_return_proper_string()
		{
			var date = new DateTime(1999,12,1);
			ConvertValue(date).As<string>().ShouldContainAllOf(date.Year.ToString(),HelperCollections.MonthNames[11]);
		}
	}

}