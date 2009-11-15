using Microsoft.Practices.Composite.Modularity;
using Ninject;
using NUnit.Framework;
using Rhino.Mocks;
using TopCalendar.UI.Modules.Registration;
using TopCalendar.Utility.Tests;

namespace TopCalendar.UI.PluginManager.Tests
{
	/// <summary>
	/// Wolanie load na chwile obecna laduje wszystkie moduly.
	/// W testach to nie przejdzie, bo musialby sie odpalac WCF itd itd.
	/// Jak bedzie zrobiona konfiguracja w pliku, problem sie rozwiaze.
	/// </summary>
	[Ignore]
	public class when_loading_modules
		: observations_for_auto_created_sut_of_type<PluginLoader>
	{
		private ModuleCatalog _moduleCatalog;

		protected override void EstablishContext()
		{
			_moduleCatalog = new ModuleCatalog();
		}

		protected override void Because()
		{
			Sut.Load(_moduleCatalog);
		}

		[Test]
		public void should_load_registration_module()
		{
			_moduleCatalog.Modules.ShouldContain(
				mi => mi.ModuleType.Equals(typeof (RegistrationModule).AssemblyQualifiedName)
			);
		}
	}
}
