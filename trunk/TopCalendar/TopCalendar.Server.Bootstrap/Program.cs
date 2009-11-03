#region

using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using TopCalendar.Server.ServiceLibrary;

#endregion

namespace TopCalendar.Server.Bootstrap
{

    /// <summary>
    /// 
    /// Bootstrapper, do ktorego zadan nalezy:
    ///     1. odpalenie i skonfigurowanie nhibernate
    ///     2. odpalenie serwisu wcf (url: http://localhost:80)
    /// 
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            TopCalendarCommunicatioinServiceHost communicatioinServiceHost
                = new TopCalendarCommunicatioinServiceHost();

            communicatioinServiceHost.RunCommunicationServiceHost();
        }
    }


    public class TopCalendarCommunicatioinServiceHost
    {
        public void RunCommunicationServiceHost()
        {
            Console.WriteLine("starting communication service host");

            Uri baseAddress2 = new Uri("http://localhost:80");
            using (ServiceHost host = new ServiceHost(typeof (TopCalendarCommunicationServiceImpl), baseAddress2))
            {
                host.AddServiceEndpoint(typeof (ITopCalendarCommunicationService), new BasicHttpBinding(), "");

                ServiceMetadataBehavior smb = new ServiceMetadataBehavior {HttpGetEnabled = true};
                host.Description.Behaviors.Add(smb);

                host.Open();

                Console.WriteLine("service started, press any key to halt");

                Console.ReadKey();
            }
        }
    }
}