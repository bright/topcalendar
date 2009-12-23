using System;
using System.Collections.ObjectModel;
using TopCalendar.Client.Connector;
using TopCalendar.Client.DataModel;
using TopCalendar.Utility;
using TopCalendar.Utility.BasicExtensions;

namespace TopCalendar.UI.Modules.WeekViewer
{
	public class WeekTaskLoader : IWeekTaskLoader
	{
		private readonly ITaskRepository _taskRepository;
		private int DaysInWeek = 7;
		private int HoursInDay = 24;

		public WeekTaskLoader(ITaskRepository taskRepository)
		{
			_taskRepository = taskRepository;
		}

		public ObservableCollection<ObservableCollection<HourTaskList>> GetTasksForWeek(DateTime time)
		{
			var result = PrepareResultList(time);
			var tasks = _taskRepository.GetTasksBetweenDates(new DateTimeRange(time.AtWeekStart(), time.AtWeekEnd()));
			tasks.Each(t => result[t.StartAt.DayInWeek()][t.StartAt.Hour].AddTask(t));
			return result;
		}

		private ObservableCollection<ObservableCollection<HourTaskList>> PrepareResultList(DateTime dayInWeek)
		{
			var result = new ObservableCollection<ObservableCollection<HourTaskList>>();
			var weekStart = dayInWeek.AtWeekStart();
			weekStart.Range(weekStart.AddDays(DaysInWeek), TimeSpan.FromDays(1))
				.Each(day =>
					{
						var hourTaskLists = new ObservableCollection<HourTaskList>();						
						day.Range(day.AddHours(HoursInDay), TimeSpan.FromHours(1))
							.Each(dt => hourTaskLists.Add(new HourTaskList(dt)));
						result.Add(hourTaskLists);
				    });			
			return result;
		}
	}
}