using TopCalendar.Server.DataLayer.Repositories;
using TopCalendar.Server.ServiceLibrary.ServiceContract.DataContract;
using TopCalendar.Utility;

namespace TopCalendar.Server.ServiceLibrary.ServiceLogic
{
	public class UpdateTaskLogic : RequestToResponseLogic<UpdateTaskRequest,BaseResponse>
	{
		private readonly ITasksRepository _tasksRepository;		

		public UpdateTaskLogic(ITasksRepository tasksRepository)
		{
			_tasksRepository = tasksRepository;			
		}

		public BaseResponse UpdateTask(UpdateTaskRequest request)
		{
			return WithinTransactionDo(s =>
           	{
           		var dto = request.Task;
           		var task = _tasksRepository.GetById(dto.Id);
				Check.Guard(task.User.Equals(request.CurrentUser), "Invalid user or task in update request");
           		task.Name = dto.Name;
           		task.Description = dto.Description;
           		task.StartAt = dto.StartAt;
           		task.FinishAt = dto.FinishAt;
           	}).OnErrorFillResposneWithException();
		}
	}
}