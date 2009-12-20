using System.Runtime.Serialization;
using TopCalendar.Server.ServiceLibrary.ServiceContract.DataContract.Dto;

namespace TopCalendar.Server.ServiceLibrary.ServiceContract.DataContract
{
	[DataContract]
	public class UpdateTaskRequest : RequestWithCredentials
	{
		[DataMember]
		public TaskDto Task { get; set; }
	}
}