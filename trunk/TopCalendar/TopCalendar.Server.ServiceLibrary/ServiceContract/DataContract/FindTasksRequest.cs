using System.Collections.Generic;
using System.Runtime.Serialization;
using TopCalendar.Server.ServiceLibrary.ServiceContract.DataContract.Dto;

namespace TopCalendar.Server.ServiceLibrary.ServiceContract.DataContract
{
    [DataContract]
    public class FindTasksRequest : RequestWithCredentials
    {
        [DataMember]
        public TaskSpecificationDto TaskSpecificationDto { get; set; }
    }

    [DataContract]
    public class FindTasksResponse : BaseResponse
    {
        [DataMember]
        public IList<TaskDto> Tasks { get; set; }
    }
}