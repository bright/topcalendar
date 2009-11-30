#region

using TopCalendar.Server.DataLayer.Entities;
using TopCalendar.Server.DataLayer.Repositories;
using TopCalendar.Server.ServiceLibrary.ServiceContract.DataContract;
using TopCalendar.Server.ServiceLibrary.ServiceContract.DataContract.Dto;

#endregion

namespace TopCalendar.Server.ServiceLibrary.ServiceLogic
{
    public class AddNewTasksLogic :
        RequestToResponseLogic<AddNewTaskRequest, AddNewTaskResponse>
    {
        private readonly ITasksRepository _tasksRepository;
        private readonly DtoMappingService _mappingService;

        public AddNewTasksLogic(ITasksRepository tasksRepository, DtoMappingService mappingService)
        {
            _tasksRepository = tasksRepository;
            _mappingService = mappingService;
        }

        public AddNewTaskResponse AddNewTask(AddNewTaskRequest addNewTaskRequest)
        {
            TaskDto taskDto = addNewTaskRequest.Task;
            Task task = _mappingService.FromDto(taskDto);

            task.User = addNewTaskRequest.CurrentUser;

            task = _tasksRepository.Add(task);

            taskDto = _mappingService.ToDto(task);

            AddNewTaskResponse successResponse = SuccessSituationResponse();
            successResponse.Task = taskDto;

            return successResponse;
        }
    }
}