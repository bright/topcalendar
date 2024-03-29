using System.Collections.Generic;
using System.ServiceModel;
using TopCalendar.Server.ServiceLibrary.ServiceBehavior;
using TopCalendar.Server.ServiceLibrary.ServiceContract.DataContract;
using TopCalendar.Server.ServiceLibrary.ServiceContract.DataContract.Dto;

namespace TopCalendar.Server.ServiceLibrary.ServiceContract
{
    [ServiceContract]
    public interface ITopCalendarCommunicationService
    {
        [OperationContract]
        [AttachValidUserInspector]
        LoginUserResponse LoginUser(LoginUserRequest loginUserRequest);

        [OperationContract]
        [FaultContract(typeof(DataAccessFault))]
        RegisterUserResponse RegisterUser(RegisterUserRequest registerUserRequest);

        [OperationContract]
        [FaultContract(typeof(DataAccessFault))]
        [AttachValidUserInspector]
        AddNewTaskResponse AddNewTask(AddNewTaskRequest addNewTaskRequest);

        [OperationContract]
        [FaultContract(typeof(DataAccessFault))]
        [AttachValidUserInspector]
        FindTasksResponse FindTasks(FindTasksRequest findTasksRequest);

    	[OperationContract]
    	[FaultContract(typeof (DataAccessFault))]
    	[AttachValidUserInspector]
    	BaseResponse RemoveTask(RemoveTaskRequest deleteTaskRequest);

    	[OperationContract]
    	[FaultContract(typeof (DataAccessFault))]
    	[AttachValidUserInspector]
    	BaseResponse UpdateTask(UpdateTaskRequest updateTaskRequest);
    }
}