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
							"Poniedzia�ek",
							"Wtorek",
							"�roda",
							"Czwartek",
							"Pi�tek",
							"Sobota",
							"Niedziela"
						}
					);
			}
		}
	}
}