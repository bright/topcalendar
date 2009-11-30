using System.Runtime.Serialization;

namespace TopCalendar.Server.ServiceLibrary.ServiceContract.DataContract
{
    [DataContract]
    public class LoginUserRequest : RequestWithCredentials
    {
    }

    [DataContract]
    public class LoginUserResponse : BaseResponse
    {
    }
}