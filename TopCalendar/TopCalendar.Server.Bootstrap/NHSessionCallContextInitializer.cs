using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using NHibernate;
using Ninject;
using ISession=NHibernate.ISession;

namespace TopCalendar.Server.Bootstrap
{
	public class NHSessionCallContextInitializer : ICallContextInitializer
	{
		private readonly IKernel _kernel;
		private readonly ISessionFactory _sessionFactory;

		public NHSessionCallContextInitializer(IKernel kernel)
		{
			_kernel = kernel;			
		}

		public object BeforeInvoke(InstanceContext instanceContext, IClientChannel channel, Message message)
		{
			return _kernel.Get<ISession>();
		}

		public void AfterInvoke(object correlationState)
		{
			var session = (ISession) correlationState;
			session.Dispose();
		}
	}
}