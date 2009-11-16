using System;
using TopCalendar.Client.Connector;
using TopCalendar.UI.Modules.MonthViewer;
using TopCalendar.UI.Modules.Registration;

namespace TopCalendar.UI.PluginManager
{
	public class PluginConfigurationProvider : IPluginConfigurationProvider
	{
		public Type[] PresentationModules
		{
			get
			{
				return new[] {typeof (RegistrationModule), typeof (MonthViewerModule)};
			}
		}

		/// <summary>
		/// Zwraca liste modulow fasady klienckiej. Moduly nie sa dostepne do konfiguracji,
		/// bo ich lista nie jest konfigurowalna - wszystkie sa potrzebne i z gory okreslone.
		/// Sa jednak wyniesione do tej klasy, zeby je ladnie zamockowac :)
		/// </summary>
		public Type[] ConnectorModules
		{
			get
			{
				return new[]
			       	{
			       		typeof (IUserRegistrator)
			       	};
			}
		}
	}
}