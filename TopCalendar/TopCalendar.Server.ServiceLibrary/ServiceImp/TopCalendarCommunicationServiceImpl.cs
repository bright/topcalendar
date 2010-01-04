#region

using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using Microsoft.Practices.ServiceLocation;
using TopCalendar.Server.ServiceLibrary.ServiceContract;
using TopCalendar.Server.ServiceLibrary.ServiceContract.DataContract;
using TopCalendar.Server.ServiceLibrary.ServiceLogic;

#endregion

namespace TopCalendar.Server.ServiceLibrary.ServiceImp
{
    public class TopCalendarCommunicationServiceImpl : ITopCalendarCommunicationService
    {
    	public TopCalendarCommunicationServiceImpl()
        {
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
			return InvokeLogicAndReturnResponse<RegisterUserRequest, RegisterUserResponse>(registerUserRequest);
        }

        public AddNewTaskResponse AddNewTask(AddNewTaskRequest addNewTaskRequest)
        {
			return InvokeLogicAndReturnResponse<AddNewTaskRequest, AddNewTaskResponse>(addNewTaskRequest);
        }

        public FindTasksResponse FindTasks(FindTasksRequest findTasksRequest)
        {
			return InvokeLogicAndReturnResponse<FindTasksRequest, FindTasksResponse>(findTasksRequest);
        }

    	public BaseResponse RemoveTask(RemoveTaskRequest deleteTaskRequest)
    	{
			return InvokeLogicAndReturnResponse<RemoveTaskRequest, BaseResponse>(deleteTaskRequest);
    	}

    	public BaseResponse UpdateTask(UpdateTaskRequest updateTaskRequest)
    	{
			return InvokeLogicAndReturnResponse<UpdateTaskRequest,BaseResponse>(updateTaskRequest);
    	}

		protected TResponse InvokeLogicAndReturnResponse<TRequest,TResponse>(TRequest request)
		{
			LogMessage(request);
			var logicHandler = ServiceLocator.Current.GetInstance<IRequestToResponseLogic<TRequest, TResponse>>();
			var response = logicHandler.Process(request);
			LogMessage(response);
			return response;
		}
		// todo: this method is quite inefficient
    	private void LogMessage(object dataContract)
    	{
    		var serializer = new DataContractSerializer(dataContract.GetType());
    		Console.WriteLine();
			using (var standardOutput = Console.OpenStandardOutput())
			using(var xw =new XmlTextWriter(standardOutput, Encoding.UTF8))
			{
				xw.Formatting = Formatting.Indented;
				xw.Indentation = 4;
				serializer.WriteObject(xw, dataContract);				
				standardOutput.Flush();
				Console.WriteLine();
				Console.WriteLine();
			}
    	}
    }
}