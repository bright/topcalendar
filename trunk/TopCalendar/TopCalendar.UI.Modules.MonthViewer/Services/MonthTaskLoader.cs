using System;
using System.Collections.ObjectModel;
using System.Linq;
using AutoMapper;
using TopCalendar.Client.Connector;
using TopCalendar.UI.Modules.MonthViewer.Model;
using TopCalendar.Utility;
using TopCalendar.Utility.BasicExtensions;

namespace TopCalendar.UI.Modules.MonthViewer.Services
{
	public class MonthTaskLoader : IMonthTaskLoader
	{
		private readonly ITaskRepository _taskRepository;
		private const int RowCount = 5;
		private const int ColumnCount = 7;
		public MonthTaskLoader(ITaskRepository taskRepository)
		{
			_taskRepository = taskRepository;
		}

		public ObservableCollection<ObservableCollection<DayTaskList>> GetTasksForMonth(DateTime date)
		{
			var monthTasks = _taskRepository.GetTasksBetweenDates(new DateTimeRange(date.AtMonthStart(),date.AtMonthEnd()));
			var monthStart = date.AtMonthStart();
			var monthEnd = date.AtMonthEnd();
			var first = (int)monthStart.DayOfWeek;
			var result = new ObservableCollection<ObservableCollection<DayTaskList>>();
			var iteratedDate = monthStart.AddDays(-((first-1 + 7)%7));
			for(int i = 0; i < RowCount; ++i)
			{
				result.Add(new ObservableCollection<DayTaskList>());
				for(int j=0;j<ColumnCount;++j)
				{
					result[i].Add(new DayTaskList(iteratedDate));
					var matchedTasks = monthTasks.Where(task =>
					                                    BasicExtensions.IsBetween(task.StartAt, iteratedDate.AtDayStart(), iteratedDate.AtDayEnd())).ToList();
					matchedTasks.Each(mt=> result[i][j].AddTask(mt));
					iteratedDate = iteratedDate.AddDays(1);
				}
			}
			return result;
		}
	}
}