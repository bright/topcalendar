#region

using System;
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

        public override AddNewTaskResponse Process(AddNewTaskRequest addNewTaskRequest)
        {
			TaskDto taskDto = addNewTaskRequest.Task;			
			// there should not be anny mapping from dtos to domain                     	
			return WithinTransactionDo(s =>
			                           	{
			                           		var task = new Task(addNewTaskRequest.CurrentUser)
			                           		        	{
			                           		        		Name = taskDto.Name,
															Description =taskDto.Description,
															FinishAt = taskDto.FinishAt,
															StartAt = taskDto.StartAt
			                           		        	};
			                           		_tasksRepository.Add(task);
			                           	})
        		.OnErrorFillResponse(r => r.Task = taskDto);            
        }		
    }
}