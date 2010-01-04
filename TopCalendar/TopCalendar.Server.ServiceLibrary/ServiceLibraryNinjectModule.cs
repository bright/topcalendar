using Ninject.Modules;
using TopCalendar.Server.ServiceLibrary.ServiceBehavior;
using TopCalendar.Server.ServiceLibrary.ServiceContract;
using TopCalendar.Server.ServiceLibrary.ServiceContract.DataContract;
using TopCalendar.Server.ServiceLibrary.ServiceImp;
using TopCalendar.Server.ServiceLibrary.ServiceLogic;
using TopCalendar.Server.ServiceLibrary.ServiceLogic.NinjectExtensions;



namespace TopCalendar.Server.ServiceLibrary
{
    public class ServiceLibraryNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<UserRegistrationLogic>().ToSelf();
            Bind<AddNewTasksLogic>().ToSelf();
            Bind<ValidUserParameterInspector>().ToSelf();
            Bind<ITopCalendarCommunicationService>().To<TopCalendarCommunicationServiceImpl>();
        	Bind(typeof (ITransactionHandler<>)).To(typeof (TransactionHandler<>));
            Bind<DtoMappingService>().ToConstant(new DtoMappingService());

        	Kernel.BindRequestToResponseLogic<UserRegistrationLogic, RegisterUserRequest, RegisterUserResponse>();
			Kernel.BindRequestToResponseLogic<AddNewTasksLogic, AddNewTaskRequest, AddNewTaskResponse>();
			Kernel.BindRequestToResponseLogic<FindTasksLogic, FindTasksRequest, FindTasksResponse>();
        	Kernel.BindRequestToResponseLogic<RemoveTaskLogic, RemoveTaskRequest, BaseResponse>();
        	Kernel.BindRequestToResponseLogic<UpdateTaskLogic, UpdateTaskRequest, BaseResponse>();
        }		
    }
}