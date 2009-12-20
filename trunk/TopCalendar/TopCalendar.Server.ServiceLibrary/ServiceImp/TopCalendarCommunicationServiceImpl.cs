#region

using System;
using Microsoft.Practices.ServiceLocation;
using TopCalendar.Server.ServiceLibrary.ServiceContract;
using TopCalendar.Server.ServiceLibrary.ServiceContract.DataContract;
using TopCalendar.Server.ServiceLibrary.ServiceLogic;

#endregion

namespace TopCalendar.Server.ServiceLibrary.ServiceImp
{
    public class TopCalendarCommunicationServiceImpl : ITopCalendarCommunicationService
    {
        private readonly UserRegistrationLogic _userRegistrationLogic;
        private readonly AddNewTasksLogic _addNewTaskLogic;
        private readonly FindTasksLogic _findTasksLogic;
    	private readonly RemoveTaskLogic _removeTaskLogic;
    	private readonly UpdateTaskLogic _updateTaskLogic;

    	public TopCalendarCommunicationServiceImpl(UserRegistrationLogic userRegistrationLogic,
                                                   AddNewTasksLogic addNewTaskLogic, FindTasksLogic findTasksLogic, RemoveTaskLogic removeTaskLogic,
												   UpdateTaskLogic updateTaskLogic
			)
        {
            _userRegistrationLogic = userRegistrationLogic;
            _addNewTaskLogic = addNewTaskLogic;
            _findTasksLogic = findTasksLogic;
        	_removeTaskLogic = removeTaskLogic;
    		_updateTaskLogic = updateTaskLogic;
        }

        public LoginUserResponse LoginUser(LoginUserRequest loginUserRequest)
        {
            Console.WriteLine("LoginUser, loginUserRequest: " + loginUserRequest);

            // jezeli sterowanie doszlo tutaj to znaczy, ze uzytkownik
            // podal poprawne dane

            // gdyby podal bledne dane to interceptor wyslalby od niego
            // komunikat o blednej parze login/password

            return new LoginUserResponse {Success = true};
        }

        public RegisterUserResponse RegisterUser(RegisterUserRequest registerUserRequest)
        {
            Console.WriteLine("RegisterUser, registerUserRequest: " + registerUserRequest);

            RegisterUserResponse registerUserResponse =
                _userRegistrationLogic.RegisterUser(registerUserRequest);

            return registerUserResponse;
        }

        public AddNewTaskResponse AddNewTask(AddNewTaskRequest addNewTaskRequest)
        {
            Console.WriteLine("AddNewTask, addNewTaskRequest: " + addNewTaskRequest);

            AddNewTaskResponse addNewTaskResponse =
                _addNewTaskLogic.AddNewTask(addNewTaskRequest);

            return addNewTaskResponse;
        }

        public FindTasksResponse FindTasks(FindTasksRequest findTasksRequest)
        {
            Console.WriteLine("FindTasks, findTasksRequest: " + findTasksRequest);

            FindTasksResponse findTasksResponse =
                _findTasksLogic.FindTasks(findTasksRequest);

            return findTasksResponse;
        }

    	public BaseResponse RemoveTask(RemoveTaskRequest deleteTaskRequest)
    	{
    		Console.WriteLine("RemoveTask, findTaskRequest" + deleteTaskRequest);

    		return _removeTaskLogic.RemoveTask(deleteTaskRequest);
    	}

    	public BaseResponse UpdateTask(UpdateTaskRequest updateTaskRequest)
    	{
			Console.WriteLine("UpdateTask, updateTaskRequest" + updateTaskRequest);

    		return _updateTaskLogic.UpdateTask(updateTaskRequest);
    	}
    }
}