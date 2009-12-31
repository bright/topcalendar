using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Microsoft.Practices.Composite;
using Microsoft.Practices.Composite.Logging;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Presentation.Regions;
using Microsoft.Practices.Composite.Presentation.Regions.Behaviors;
using Microsoft.Practices.Composite.Regions;
using Ninject;
using NinjectContrib.CompositePresentation.KernelModules;
using NinjectContrib.CompositePresentation.Properties;
using TopCalendar.UI.Infrastructure.Regions;
using ActivationException=Ninject.ActivationException;


namespace NinjectContrib.CompositePresentation
{
	/// <summary>
	/// Base class that provides a basic bootstrapping sequence that
	/// registers most of the Composite Application Library assets
	/// in a <see cref="IKernel"/>.
	/// </summary>
	/// <remarks>
	/// This class must be overriden to provide application specific configuration.
	/// </remarks>
	public abstract class NinjectBootstrapper
	{
		private readonly ILoggerFacade _loggerFacade = new TraceLogger();
		private bool _useDefaultConfiguration = true;

		/// <summary>
		/// Gets the default <see cref="IKernel"/> for the application.
		/// </summary>
		/// <value>The default <see cref="IKernel"/> instance.</value>
		public IKernel Kernel { get; private set; }

		/// <summary>
		/// Gets the default <see cref="ILoggerFacade"/> for the application.
		/// </summary>
		/// <value>A <see cref="ILoggerFacade"/> instance.</value>
		protected virtual ILoggerFacade LoggerFacade
		{
			get { return this._loggerFacade; }
		}

		/// <summary>
		/// Runs the bootstrapper process.
		/// </summary>
		public void Run() { Run(true); }

		/// <summary>
		/// Run the bootstrapper process.
		/// </summary>
		/// <param name="useDefaultConfiguration">If <see langword="true"/>, registers default Composite Application Library services in the container. This is the default behavior.</param>
		public void Run(bool useDefaultConfiguration)
		{
			this._useDefaultConfiguration = useDefaultConfiguration;
			var logger = LoggerFacade;
			if (logger == null)
				throw new InvalidOperationException(Resources.NullLoggerFacadeException);

			logger.Log("Creating Ninject kernel", Category.Debug, Priority.Low);
			Kernel = CreateKernel();
			if (Kernel == null)
				throw new InvalidOperationException(Resources.NullUnityContainerException);

			logger.Log("Configuring kernel", Category.Debug, Priority.Low);

			ConfigureKernel();

			logger.Log("Configuring region adapters", Category.Debug, Priority.Low);

			ConfigureRegionAdapterMappings();
			ConfigureDefaultRegionBehaviors();
			RegisterFrameworkExceptionTypes();

			logger.Log("Creating shell", Category.Debug, Priority.Low);
			var shell = CreateShell();

			if (shell != null)
			{
				RegionManager.SetRegionManager(shell, Kernel.Get<IRegionManager>());
				RegionManager.UpdateRegions();
			}

			logger.Log("Initializing modules", Category.Debug, Priority.Low);
			InitializeModules();

			logger.Log("Bootstrapper sequence completed", Category.Debug, Priority.Low);
		}

		/// <summary>
		/// Configures the <see cref="IKernel"/>. May be overwritten in a derived class to add specific
		/// type mappings required by the application.
		/// </summary>
		protected virtual void ConfigureKernel()
		{			
			Kernel.Load(new LoggerFacadeModule(LoggerFacade));

			if (this._useDefaultConfiguration)
			{
				Kernel.Load(new DefaultConfigurationModule());
			}

			Kernel.Load(new ModuleCatalogModule(GetModuleCatalog()));
		}

		/// <summary>
		/// Configures the <see cref="IRegionBehaviorFactory"/>. This will be the list of default
		/// behaviors that will be added to a region. 
		/// </summary>
		protected virtual IRegionBehaviorFactory ConfigureDefaultRegionBehaviors()
		{
			try
			{
				var defaultRegionBehaviorTypesDictionary = Kernel.Get<IRegionBehaviorFactory>();
				if (defaultRegionBehaviorTypesDictionary != null)
				{
					defaultRegionBehaviorTypesDictionary.AddIfMissing(AutoPopulateRegionBehavior.BehaviorKey, typeof(AutoPopulateRegionBehavior));
					defaultRegionBehaviorTypesDictionary.AddIfMissing(BindRegionContextToDependencyObjectBehavior.BehaviorKey, typeof(BindRegionContextToDependencyObjectBehavior));
					//defaultRegionBehaviorTypesDictionary.AddIfMissing("ActiveAware", typeof(RegionActiveAwareBehavior));
                    defaultRegionBehaviorTypesDictionary.AddIfMissing(LastViewIsActiveRegionBehavior.BehaviorKey, typeof(LastViewIsActiveRegionBehavior));
					defaultRegionBehaviorTypesDictionary.AddIfMissing(SyncRegionContextWithHostBehavior.BehaviorKey, typeof(SyncRegionContextWithHostBehavior));
					defaultRegionBehaviorTypesDictionary.AddIfMissing(RegionManagerRegistrationBehavior.BehaviorKey, typeof(RegionManagerRegistrationBehavior));
				}
				return defaultRegionBehaviorTypesDictionary;
			}
			catch (ActivationException)
			{
				return null;
			}
		}

		/// <summary>
		/// Configures the default region adapter mappings to use in the application, in order
		/// to adapt UI controls defined in XAML to use a region and register it automatically.
		/// May be overwritten in a derived class to add specific mappings required by the application.
		/// </summary>
		/// <returns>The <see cref="RegionAdapterMappings"/> instance containing all the mappings.</returns>
		protected virtual RegionAdapterMappings ConfigureRegionAdapterMappings()
		{
			RegionAdapterMappings regionAdapterMappings = null;
			try
			{
				regionAdapterMappings = Kernel.Get<RegionAdapterMappings>();
#if SILVERLIGHT
		        regionAdapterMappings.RegisterMapping(typeof(TabControl), Kernel.Get<TabControlRegionAdapter>());
#endif
				regionAdapterMappings.RegisterMapping(typeof(Selector), Kernel.Get<SelectorRegionAdapter>());
				regionAdapterMappings.RegisterMapping(typeof(ItemsControl), Kernel.Get<ItemsControlRegionAdapter>());
				regionAdapterMappings.RegisterMapping(typeof(ContentControl), Kernel.Get<ContentControlRegionAdapter>());
			}
			catch (ActivationException)
			{
				// Ignore and just return null
			}
			return regionAdapterMappings;
		}

		/// <summary>
		/// Initializes the modules. May be overwritten in a derived class to use custom 
		/// module loading and avoid using an <seealso cref="IModuleManager"/> and 
		/// <seealso cref="IModuleCatalog"/>.
		/// </summary>
		protected virtual void InitializeModules()
		{
			IModuleManager manager;

			try
			{
				manager = Kernel.Get<IModuleManager>();
			}
			catch (ActivationException ex)
			{
				if (ex.Message.Contains("IModuleCatalog"))
					throw new InvalidOperationException(Resources.NullModuleCatalogException);

				throw;
			}

			manager.Run();
		}

		/// <summary>
		/// Registers in the <see cref="IKernel"/> the <see cref="Type"/> of the Exceptions
		/// that are not considered root exceptions by the <see cref="ExceptionExtensions"/>.
		/// </summary>
		protected virtual void RegisterFrameworkExceptionTypes()
		{
			ExceptionExtensions.RegisterFrameworkExceptionType(typeof(Microsoft.Practices.ServiceLocation.ActivationException));
			ExceptionExtensions.RegisterFrameworkExceptionType(typeof(ActivationException));
		}

		/// <summary>
		/// Creates the <see cref="IKernel"/> that will be used as the default container.
		/// </summary>
		/// <returns>A new instance of <see cref="IKernel"/>.</returns>
		protected virtual IKernel CreateKernel() { return new StandardKernel(); }

		/// <summary>
		/// Returns the module enumerator that will be used to initialize the modules.
		/// </summary>
		/// <remarks>
		/// When using the default initialization behavior, this method must be overwritten by a derived class.
		/// </remarks>
		/// <returns>An instance of <see cref="IModuleCatalog"/> that will be used to initialize the modules.</returns>
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		protected virtual IModuleCatalog GetModuleCatalog() { return null; }

		/// <summary>
		/// Creates the shell or main window of the application.
		/// </summary>
		/// <returns>The shell of the application.</returns>
		/// <remarks>
		/// If the returned instance is a <see cref="DependencyObject"/>, the
		/// <see cref="NinjectBootstrapper"/> will attach the default <seealso cref="IRegionManager"/> of
		/// the application in its <see cref="RegionManager.RegionManagerProperty"/> attached property
		/// in order to be able to add regions by using the <seealso cref="RegionManager.RegionNameProperty"/>
		/// attached property from XAML.
		/// </remarks>
		protected abstract DependencyObject CreateShell();
	}
}