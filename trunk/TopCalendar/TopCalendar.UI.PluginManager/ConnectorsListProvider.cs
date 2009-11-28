using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.Composite.Modularity;
using TopCalendar.Client.Connector;

namespace TopCalendar.UI.PluginManager
{
	public class ConnectorsListProvider : IConnectorsListProvider
	{
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