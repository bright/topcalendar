using System;
using System.Collections.Generic;
using NUnit.Framework;
using Rhino.Mocks;
using TopCalendar.Utility.Tests;

namespace TopCalendar.UI.PluginManager.Tests
{
	public class when_asking_for_presentation_modules
		: observations_for_auto_created_sut_of_type<PluginConfigurationProvider>
	{
		private Type[] _result;

		protected override void EstablishContext()
		{
			var plugin = new Plugin()
	          	{
					Assembly = "../../../../TopCalendar.UI.Modules.Registration/obj/Debug/TopCalendar.UI.Modules.Registration.dll",
					IsActive = true
	          	};

			Dependency<IPluginConfigurationHandler>().Stub(
				pch => pch.Plugins
			).Return(new List<Plugin> { plugin });
		}

		protected override void Because()
		{
			_result = Sut.PresentationModules;
		}

		[Test]
		public void one_module_should_be_returned()
		{
			_result.ShouldHaveCount(1);
		}

		[Test]
		public void registration_module_should_be_listed()
		{
			_result.ShouldContain(t => t.Name == "RegistrationModule");
		}
	}
}