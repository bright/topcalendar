using System;
using System.Runtime.Serialization;

namespace TopCalendar.Server.ServiceLibrary.ServiceContract.DataContract
{
    [DataContract]
    public abstract class BaseResponse
    {
        [DataMember]
        public bool Success { get; set; }

        [DataMember]
        public String StatusReason { get; set; }

    }
  
}