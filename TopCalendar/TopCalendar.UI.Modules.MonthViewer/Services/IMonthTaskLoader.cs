using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AutoMapper;
using TopCalendar.Client.Connector;
using TopCalendar.Client.Connector.Model;
using TopCalendar.UI.Modules.MonthViewer.Model;
using TopCalendar.Utility;
using TopCalendar.Utility.BasicExtensions;

namespace TopCalendar.UI.Modules.MonthViewer.Services
{
	public interface IMonthTaskLoader
	{
		ObservableCollection<ObservableCollection<ObservableCollection<MonthTask>>> GetTasksForMonth(DateTime date, int rowCount, int dayCount);
	}

	public class MonthTaskLoader : IMonthTaskLoader
	{
		private readonly ITaskRepository _taskRepository;

		public MonthTaskLoader(ITaskRepository taskRepository)
		{
			_taskRepository = taskRepository;
		}

		public ObservableCollection<ObservableCollection<ObservableCollection<MonthTask>>> GetTasksForMonth(DateTime date, int rowCount, int columnCount)
		{
			var monthTasks = _taskRepository.GetTasksBetweenDates(date.AtMonthStart(), date.AtMonthEnd());
			var monthStart = date.AtMonthStart();
			var monthEnd = date.AtMonthEnd();
			var first = (int)monthStart.DayOfWeek;
			var result = new ObservableCollection<ObservableCollection<ObservableCollection<MonthTask>>>();
			var iteratedDate = monthStart.AddDays(-((first-1 + 7)%7));
			for(int i = 0; i < rowCount; ++i)
			{
				result.Add(new ObservableCollection<ObservableCollection<MonthTask>>());
				for(int j=0;j<columnCount;++j)
				{
					result[i].Add(new ObservableCollection<MonthTask>());
					var matchedTasks = monthTasks.Where(task =>
						task.StartAt.IsBetween(iteratedDate.AtDayStart(), iteratedDate.AtDayEnd())).ToList();
					matchedTasks.Each(mt=> result[i][j].Add(Mapper.Map<Task,MonthTask>(mt)));
					iteratedDate = iteratedDate.AddDays(1);
				}
			}
			return result;
		}
	}
}