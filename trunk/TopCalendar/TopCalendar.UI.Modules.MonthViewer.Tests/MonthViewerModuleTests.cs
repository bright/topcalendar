using System;
using Microsoft.Practices.Composite.Presentation.Regions;
using Microsoft.Practices.Composite.Regions;
using NUnit.Framework;
using Rhino.Mocks;
using TopCalendar.UI.Infrastructure;
using TopCalendar.Utility.Tests;

namespace TopCalendar.UI.Modules.MonthViewer.Tests
{
	/// <summary>
	/// ten test nie jest dobry, bo zagląda w obca implementacje,
	/// sluzy tylko pokazaniu jak mozna testowac z automokcingkernelem ;-p
	/// chodzi o to zeby testowac przy uzyciu Arrange, Act, Assert
	/// czyli w EstablishContext przygotowujemy srodowisko
	/// w Because wykonujemy to co testujemy
	/// no w testach umieszczamy tylko assercje
	/// uwaga ! domyslnie wszystko co dostaniemy przez wywolanie Depenedncy 
	/// bedzie w stanie Replay, mozna to zminienic uzywajac metody Stub/DynamickMock w establish context
	/// </summary>
	public class when_initializing_month_viewer_module
		: observations_for_auto_created_sut_of_type<MonthViewerModule>
	{
		private IMonthView _monthView;
		private IRegionManager _regionManger;

		protected override void EstablishContext()
		{
			Stub<IMonthViewPersentationModel>()
				.Stub(mvp => mvp.View).Return(Stub<IMonthView>());
		}

		protected override void Because()
		{
			Sut.Initialize();
		}

		[Test]
		public void should_register_view_with_main_conent()
		{
			Dependency<IRegionViewRegistry>()
				.AssertWasCalled(irvr=> irvr.RegisterViewWithRegion(
					Arg.Is(RegionNames.MainContent), Arg<Func<object>>.Is.Anything));
		}

		[Test]
		public void should_register_month_view_with_main_content()
		{
			Dependency<IRegionViewRegistry>()
				.AssertWasCalled(irvr => irvr.RegisterViewWithRegion(
             		Arg.Is(RegionNames.MainContent),
             		Arg<Func<object>>.Matches(
             			o => (o.Invoke() as IMonthView) != null
						)));
					
		}
	}
	
}
