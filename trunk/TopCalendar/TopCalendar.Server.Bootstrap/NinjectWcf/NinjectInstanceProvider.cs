using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using Ninject;

namespace TopCalendar.Server.Bootstrap.NinjectWcf
{
    public class NinjectInstanceProvider : IInstanceProvider
    {
        private readonly Type serviceType;
        public NinjectInstanceProvider(Type serviceType)
        {
            this.serviceType = serviceType;
        }
        public object GetInstance(InstanceContext instanceContext)
        {
            return GetInstance(instanceContext, null);
        }
        public object GetInstance(InstanceContext instanceContext, Message message)
        {
            return KernelContainer.Kernel.Get(serviceType);
        }
        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {

        }
    }
}