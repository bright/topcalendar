using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TopCalendar.Utility.UI
{	

	public class HelperCollections
	{
		public static ObservableCollection<string> WeekDayNames
		{
			get
			{
				return new ObservableCollection<string>(
					new[]
						{
							"Poniedzia³ek",
							"Wtorek",
							"Œroda",
							"Czwartek",
							"Pi¹tek",
							"Sobota",
							"Niedziela"
						}
					);
			}
		}

		public static ObservableCollection<string> MonthNames
		{
			get
			{
				return new ObservableCollection<string>(
					new[]
						{
							"Styczeñ",
							"Luty",
							"Marzec",
							"Kwiecieñ",
							"Maj",
							"Czerwiec",
                            "Lipiec",
							"Sierpieñ",
							"Wrzesieñ",
							"PaŸdziernik",
							"Listopad",
							"Grudzieñ"
						}
					);
			}
		}
	}
}