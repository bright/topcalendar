using System.Runtime.Serialization;

namespace TopCalendar.Server.ServiceLibrary
{
    [DataContract]
    public abstract class BaseResponse
    {
        [DataMember]
        public bool Success { get; set; }
    }
}