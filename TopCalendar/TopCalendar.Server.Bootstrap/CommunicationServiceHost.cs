using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using TopCalendar.Server.Bootstrap.NinjectWcf;
using TopCalendar.Server.ServiceLibrary.ServiceContract;
using TopCalendar.Server.ServiceLibrary.ServiceImp;

namespace TopCalendar.Server.Bootstrap
{
    public class CommunicationServiceHost
    {
        public void RunCommunicationServiceHost()
        {
            Console.WriteLine("starting communication service host");

            Uri baseAddress2 = new Uri("http://localhost:80");
            using (ServiceHost host = new NinjectServiceHost(typeof (TopCalendarCommunicationServiceImpl), baseAddress2))
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