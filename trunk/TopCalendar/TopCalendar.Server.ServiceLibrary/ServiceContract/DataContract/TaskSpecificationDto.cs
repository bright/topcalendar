using System;
using System.Runtime.Serialization;

namespace TopCalendar.Server.ServiceLibrary.ServiceContract.DataContract
{
    [DataContract]
    public class TaskSpecificationDto
    {
        [DataMember]
        public DateTime? StartAtFrom { get; set; }
        [DataMember]
        public DateTime? StartAtTo { get; set; }

    }
}