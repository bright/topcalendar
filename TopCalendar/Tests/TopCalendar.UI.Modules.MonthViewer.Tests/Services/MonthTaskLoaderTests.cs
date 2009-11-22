using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AutoMapper;
using NUnit.Framework;
using Rhino.Mocks;
using TopCalendar.Client.Connector;
using TopCalendar.Client.Connector.Model;
using TopCalendar.UI.Modules.MonthViewer.Configuration;
using TopCalendar.UI.Modules.MonthViewer.Model;
using TopCalendar.UI.Modules.MonthViewer.Services;
using TopCalendar.Utility;
using TopCalendar.Utility.Tests;
using TopCalendar.Utility.BasicExtensions;

namespace TopCalendar.UI.Modules.MonthViewer.Tests.Services
{
	[TestFixture]
	public class when_task_loader_gets_task_for_month	
		: observations_for_auto_created_sut_of_type<MonthTaskLoader>
	{
		private ObservableCollection<ObservableCollection<DayTaskList>> _result;
		private DateTime _date;
		private int _columnCount;
		private int _rowCount;
		private IList<Task> _listFromRepository;

		protected override void Because()
		{
			_result = Sut.GetTasksForMonth(_date);
		}

		protected override void EstablishContext()
		{
			TasksRunner.Get().Execute<TaskToMonthViewTaskMapping>();
			_date = new DateTime(2009,12,19);
			_rowCount = 5;
			_columnCount = 7;
			_listFromRepository = new List<Task> { new Task{StartAt = _date.AddDays(1), Name="SecondTask"}, new Task(){StartAt = _date, Name="FirstTask"}};			
			Dependency<ITaskRepository>()
				.Stub(repo => repo.GetTasksBetweenDates(DateTime.Now, DateTime.Now))
				.IgnoreArguments()
				.Return(_listFromRepository);
		}

		protected override void AfterEachObservation()
		{
			base.AfterEachObservation();
			Mapper.Reset();
		}

		[Test]
		public void should_get_tasks_from_repository()
		{
			Dependency<ITaskRepository>()
				.AssertWasCalled(repo=> repo.GetTasksBetweenDates(
						Arg.Is(_date.AtMonthStart()),
						Arg.Is(_date.AtMonthEnd())
					));
		}

		[Test]
		public void should_return_list_with_proper_row_number()
		{
			_result.ShouldHaveCount( _rowCount);
		}

		[Test]
		public void should_return_list_where_each_element_has_proper_element_count()
		{
			_result.Each(subList=> subList.ShouldHaveCount(_columnCount));
		}

		[Test]
		public void should_return_matrix_containing_task_in_proper_cels()
		{
			_result[2][5].TaskList.ShouldContain(mt=> mt.Name.Equals(_listFromRepository[1].Name));
			_result[2][5].Day.Date.ShouldEqual(_listFromRepository[1].StartAt.Date);
			_result[2][6].TaskList.ShouldContain(mt => mt.Name.Equals(_listFromRepository[0].Name));
			_result[2][6].Day.Date.ShouldEqual(_listFromRepository[0].StartAt.Date);
		}


	}
}