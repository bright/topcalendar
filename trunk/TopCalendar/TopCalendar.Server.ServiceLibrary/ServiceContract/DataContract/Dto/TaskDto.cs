#region

using System;
using System.Runtime.Serialization;

#endregion

namespace TopCalendar.Server.ServiceLibrary.ServiceContract.DataContract.Dto
{
    [DataContract]
    public class TaskDto
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public DateTime StartAt { get; set; }

        [DataMember]
        public DateTime FinishAt { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}