#region

using System;
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

        public TopCalendarCommunicationServiceImpl(UserRegistrationLogic userRegistrationLogic,
                                                   AddNewTasksLogic addNewTaskLogic, FindTasksLogic findTasksLogic)
        {
            _userRegistrationLogic = userRegistrationLogic;
            _addNewTaskLogic = addNewTaskLogic;
            _findTasksLogic = findTasksLogic;
        }

        public LoginUserResponse LoginUser(LoginUserRequest loginUserRequest)
        {
            Console.WriteLine("LoginUser, loginUserRequest: " + loginUserRequest);

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
    }
}