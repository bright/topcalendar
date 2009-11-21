using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TopCalendar.UI.Modules.MonthViewer.Model;
using TopCalendar.Utility;

namespace TopCalendar.UI.Modules.MonthViewer.Services
{
	public interface IMonthTaskLoader
	{
		ObservableCollection<ObservableCollection<DayTaskList>> GetTasksForMonth(DateTime date, int rowCount, int dayCount);
	}
}