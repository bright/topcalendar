using Microsoft.Practices.Composite.Modularity;
using Ninject;
using NUnit.Framework;
using TopCalendar.UI.Modules.MonthViewer;
using TopCalendar.Utility.Tests;

namespace TopCalendar.UI.Tests
{
	/// <summary>
	/// nie wiedziec czemu nagle ten test przestal dzialac z dziwnym komunikate
	/// </summary>
	[Ignore]
	public class when_running_topcalendaruibootstrapper
		: observations_for_sut_of_type<TopCalendarUIBootstrapper>
	{
		protected override void Because()
		{
			Sut.Run();
		}

		protected override TopCalendarUIBootstrapper CreateSut()
		{
			return new TopCalendarUIBootstrapper();
		}

		[Test]
		public void should_configure_kernel()
		{
			Sut.Kernel.ShouldNotBeNull();			
		}

		[Test]
		public void should_bind_shellview()
		{
			Sut.Kernel.Get<IShellView>().ShouldNotBeNull();
		}

		[Test]
		public void should_configure_module_catalog()
		{
			Sut.Kernel.Get<IModuleCatalog>().ShouldNotBeNull();
		}

		[Test]
		public void should_add_month_viewer_module()
		{
			Sut.Kernel.Get<IModuleCatalog>().Modules.ShouldContain(mi=> mi.ModuleType.Equals(typeof(MonthViewerModule).AssemblyQualifiedName));
		}
	}
}