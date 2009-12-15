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

		public BaseResponse RemoveTask(RemoveTaskRequest request)
		{
			return ExecuteAndReturn(request, r => _taskRepository.Remove(_taskRepository.GetById(request.TaskId)));
		}
	}
}