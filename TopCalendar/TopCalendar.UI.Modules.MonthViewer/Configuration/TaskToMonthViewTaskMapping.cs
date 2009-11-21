using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AutoMapper;
using TopCalendar.Client.Connector.Model;
using TopCalendar.UI.Modules.MonthViewer.Model;
using TopCalendar.Utility;

namespace TopCalendar.UI.Modules.MonthViewer.Configuration
{
	public class TaskToMonthViewTaskMapping : IBootstrapperTask
	{
		public void Execute()
		{
			Mapper.CreateMap<Task, MonthTask>();
			Mapper.CreateMap<IEnumerable<Task>, ObservableCollection<MonthTask>>()
				.ConvertUsing( 
				tasks=> 
					new ObservableCollection<MonthTask>(
							Mapper.Map<IEnumerable<Task>,IEnumerable<MonthTask>>(tasks)
						)
				);
		}
	}
}