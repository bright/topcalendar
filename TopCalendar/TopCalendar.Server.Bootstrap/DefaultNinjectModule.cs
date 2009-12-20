#region

using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using CommonServiceLocator.NinjectAdapter;
using Microsoft.Practices.ServiceLocation;
using Ninject;
using Ninject.Modules;
using ISession=NHibernate.ISession;

#endregion

namespace TopCalendar.Server.Bootstrap
{
    public class DefaultNinjectModule : NinjectModule
    {
        public override void Load()
        {
			Bind<IServiceLocator>().To<NinjectServiceLocator>().InSingletonScope();
        	Bind<ISession>().ToProvider<NHSessionProvider>().InScope(ctx => OperationContext.Current);
			ServiceLocator.SetLocatorProvider(()=> Kernel.Get<IServiceLocator>());
			Bind<ICallContextInitializer>().To<NHSessionCallContextInitializer>().InSingletonScope();			
        }
    }
}