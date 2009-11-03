using System.ServiceModel;

namespace TopCalendar.Server.ServiceLibrary
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