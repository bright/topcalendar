using System.Runtime.Serialization;

namespace TopCalendar.Server.ServiceLibrary.ServiceContract.DataContract
{
    [DataContract]
    public class RegisterUserRequest : RequestWithCredentials
    {
        ///
        /// UserCredentials sluza tutaj jako dane do rejestracji
        /// 
    }

    [DataContract]
    public class RegisterUserResponse : BaseResponse
    {
    }
}