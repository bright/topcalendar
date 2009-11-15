using System;
using System.ServiceModel;

namespace TopCalendar.Server.Bootstrap.NinjectWcf
{
    public class NinjectServiceHost : ServiceHost
    {
        public NinjectServiceHost()
        {
        }
        public NinjectServiceHost(Type serviceType, params Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        {
        }
        protected override void OnOpening()
        {
            Description.Behaviors.Add(new NinjectServiceBehavior());
            base.OnOpening();
        }
    }
}