using System.Runtime.Serialization;

namespace TopCalendar.Server.ServiceLibrary.ServiceContract.DataContract
{
    [DataContract]
    public class UserCredentials
    {

        [DataMember]
        public string Login { get; set; }

        [DataMember]
        public string Password { get; set; }

    }
}