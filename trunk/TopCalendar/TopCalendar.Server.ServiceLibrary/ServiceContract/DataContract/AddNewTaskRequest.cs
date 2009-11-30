#region

using System.Runtime.Serialization;
using TopCalendar.Server.ServiceLibrary.ServiceContract.DataContract.Dto;

#endregion

namespace TopCalendar.Server.ServiceLibrary.ServiceContract.DataContract
{
    [DataContract]
    public class AddNewTaskRequest : RequestWithCredentials
    {
        [DataMember]
        public TaskDto Task { get; set; }
    }


    [DataContract]
    public class AddNewTaskResponse : BaseResponse
    {
        public TaskDto Task { get; set; }
    }
}