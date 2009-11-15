using Microsoft.Practices.Composite.Modularity;
using Ninject;
using NUnit.Framework;
using TopCalendar.UI.Modules.MonthViewer;
using TopCalendar.Utility.Tests;

namespace TopCalendar.UI.Tests
{
	/// <summary>
	/// Baaardzo brzydkie rozwiazanie, ale nie da sie chyba tego zrobic lepiej.
	/// Test bootstrapera nie powinien ladowac pluginow, bo to inna bajka.
	/// Nie da sie zamockowac metody wewnatrz Sut-a, nie jest chyba tez dobrym pomyslem
	/// wynoszenie ladowania pluginow poza bootstraper, bo przeciez po to on jest.
	/// </summary>
	public class TopCalendarUIBootstrapperForTest : TopCalendarUIBootstrapper
	{
		protected override void LoadPlugins()
		{
		}
	}

	public class when_running_topcalendaruibootstrapper
		: observations_for_sut_of_type<TopCalendarUIBootstrapper>
	{
		protected override void Because()
		{
			Sut.Run();
		}

		protected override TopCalendarUIBootstrapper CreateSut()
		{
			return new TopCalendarUIBootstrapperForTest();
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
	}
}