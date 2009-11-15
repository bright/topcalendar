using System.ServiceModel;
using TopCalendar.Server.ServiceLibrary.ServiceContract.DataContract;

namespace TopCalendar.Server.ServiceLibrary.ServiceContract
{
    [ServiceContract]
    public interface ITopCalendarCommunicationService
    {
        [OperationContract]
        CheckUserResponse CheckUser(CheckUserRequest checkUserRequest);

        [OperationContract]
        RegisterUserResponse RegisterUser(RegisterUserRequest registerUserRequest);

        // TODO: Add your service operations here
    }
}