using Ninject.Modules;

namespace NinjectContrib.CompositePresentation.KernelModules
{
	#region #using Directives

	using Microsoft.Practices.Composite.Logging;

	using Ninject;

	#endregion

	/// <summary>
	/// A simple module to bind the default <seealso cref="ILoggerFacade"/>
	/// </summary>
	public class LoggerFacadeModule : Module
	{
		private readonly ILoggerFacade loggerFacade;

		/// <summary>
		/// Instantiates a new <seealso cref="LoggerFacadeModule"/>
		/// by passing in the default <seealso cref="ILoggerFacade"/>
		/// </summary>
		/// <param name="loggerFacade">The default <seealso cref="ILoggerFacade"/></param>
		public LoggerFacadeModule(ILoggerFacade loggerFacade) { this.loggerFacade = loggerFacade; }

		/// <summary>
		/// Loads the module into the kernel.
		/// </summary>
		public override void Load()
		{
		    Bind<ILoggerFacade>().ToConstant(this.loggerFacade);
		}
	}
}