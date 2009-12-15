using System.Runtime.Serialization;

namespace TopCalendar.Server.ServiceLibrary.ServiceContract.DataContract
{
	[DataContract]
	public class RemoveTaskRequest : RequestWithCredentials
	{
		[DataMember]
		public int TaskId { get; set; }
	}
}