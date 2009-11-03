#region

using System.Runtime.Serialization;

#endregion

namespace TopCalendar.Server.ServiceLibrary
{
    // NOTE: If you change the interface name "ITopCalendarCommunicationService" here, you must also update the reference to "ITopCalendarCommunicationService" in App.config.

    // Use a data contract as illustrated in the sample below to add composite types to service operations

    [DataContract]
    public class CheckUserResponse : BaseResponse
    {
    }
}