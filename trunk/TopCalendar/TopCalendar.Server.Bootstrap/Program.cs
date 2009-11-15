#region

#endregion

#region

using Ninject;
using TopCalendar.Server.Bootstrap.NinjectWcf;
using TopCalendar.Server.DataLayer;
using TopCalendar.Server.ServiceLibrary;

#endregion

namespace TopCalendar.Server.Bootstrap
{
    /// <summary>
    /// 
    /// Bootstrapper, do ktorego zadan nalezy:
    ///     1. odpalenie i skonfigurowanie ninject
    ///     2. odpalenie serwisu wcf (url: http://localhost:80)
    /// 
    /// </summary>
    public class Program
    {

        public static void Main(string[] args)
        {
            InitiateNinject();
            RunCommunicationService();
        }

        private static void InitiateNinject()
        {
            KernelContainer.Kernel = new StandardKernel();
            KernelContainer.Kernel.LoadModulesFromAssembly(typeof (DataLayerNinjectModule).Assembly);
            KernelContainer.Kernel.LoadModulesFromAssembly(typeof (ServiceLibraryNinjectModule).Assembly);
        }

        private static void RunCommunicationService()
        {
            CommunicationServiceHost communicationServiceHost
                = new CommunicationServiceHost();

            communicationServiceHost.RunCommunicationServiceHost();
        }
    }
}