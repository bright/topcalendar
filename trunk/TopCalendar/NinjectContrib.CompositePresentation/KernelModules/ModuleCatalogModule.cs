using Ninject.Modules;

namespace NinjectContrib.CompositePresentation.KernelModules
{
	#region #using Directives

	using Microsoft.Practices.Composite.Modularity;

	using Ninject;

	#endregion

	/// <summary>
	/// A simple module to bind the default <seealso cref="IModuleCatalog"/>
	/// </summary>
	public class ModuleCatalogModule : Module
	{
		private readonly IModuleCatalog moduleCatalog;

		/// <summary>
		/// Instantiates a new <seealso cref="ModuleCatalogModule"/>
		/// by passing in the default <seealso cref="IModuleCatalog"/>
		/// </summary>
		/// <param name="moduleCatalog">The default <seealso cref="IModuleCatalog"/></param>
		public ModuleCatalogModule(IModuleCatalog moduleCatalog) { this.moduleCatalog = moduleCatalog; }

		/// <summary>
		/// Loads the module into the <seealso cref="IKernel"/>.
		/// </summary>
		public override void Load()
		{
			if (this.moduleCatalog != null)
				Bind<IModuleCatalog>().ToConstant(this.moduleCatalog);
		}
	}
}