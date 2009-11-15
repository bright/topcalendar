using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;

namespace TopCalendar.Server.Bootstrap.NinjectWcf
{
    public class NinjectServiceHostFactory : ServiceHostFactory
    {
        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            return new NinjectServiceHost(serviceType, baseAddresses);
        }
    }
}
