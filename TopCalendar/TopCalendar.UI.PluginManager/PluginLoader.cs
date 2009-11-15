using Microsoft.Practices.Composite.Modularity;
using Ninject;
using TopCalendar.Client.Connector;
using TopCalendar.UI.Modules.MonthViewer;
using TopCalendar.UI.Modules.Registration;

namespace TopCalendar.UI.PluginManager
{
	public class PluginLoader : IPluginLoader
	{
		private readonly IKernel _kernel;

		public PluginLoader(IKernel kernel)
		{
			_kernel = kernel;
		}

		public void Load(ModuleCatalog moduleCatalog)
		{
			moduleCatalog.AddModule(typeof(RegistrationModule));
			moduleCatalog.AddModule(typeof(MonthViewerModule));

			_kernel.LoadModulesFromAssembly(typeof(IUserRegistrator).Assembly);
		}
	}
}
