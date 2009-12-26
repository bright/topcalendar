using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TopCalendar.Utility.BasicExtensions;

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

		public static ObservableCollection<DateTime> HoursInDay
		{
			get
			{
				return new ObservableCollection<DateTime>(
						DateTime.Now.AtDayStart().Range(DateTime.Now.AtDayEnd(), TimeSpan.FromHours(1))
					);
			}
		}
	}
}