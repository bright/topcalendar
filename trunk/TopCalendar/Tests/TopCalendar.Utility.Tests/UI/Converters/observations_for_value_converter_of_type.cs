using System.Windows.Data;

namespace TopCalendar.Utility.Tests.UI.Converters
{
	public abstract class observations_for_value_converter_of_type<TConverter>
		: observations_for_auto_created_sut_of_type<TConverter>
		where TConverter : class, IValueConverter
	{
		protected object ConvertValue(object toConvert)
		{
			return Sut.Convert(toConvert, null, null, null);
		}

		protected override void Because()
		{			
		}
	}
}