using TopCalendar.Server.DataLayer.Repositories;
using TopCalendar.Server.ServiceLibrary.ServiceContract.DataContract;

namespace TopCalendar.Server.ServiceLibrary.ServiceLogic
{
	public class RemoveTaskLogic : RequestToResponseLogic<RemoveTaskRequest,BaseResponse>
	{
		private readonly ITasksRepository _taskRepository;

		public RemoveTaskLogic(ITasksRepository taskRepository)
		{
			_taskRepository = taskRepository;
		}

		public override BaseResponse Process(RemoveTaskRequest request)
		{
			return WithinTransactionDo(s => 
			{
				var task = _taskRepository.GetById(request.TaskId);				
			    _taskRepository.Remove(task);
			}).OnErrorFillResposneWithException();			
		}
	}
}