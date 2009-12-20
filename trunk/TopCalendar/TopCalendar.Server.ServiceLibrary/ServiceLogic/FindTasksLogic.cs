#region

using System.Collections.Generic;
using TopCalendar.Server.DataLayer.Entities;
using TopCalendar.Server.DataLayer.Repositories;
using TopCalendar.Server.DataLayer.Specifications;
using TopCalendar.Server.ServiceLibrary.ServiceContract.DataContract;
using TopCalendar.Server.ServiceLibrary.ServiceContract.DataContract.Dto;

#endregion

namespace TopCalendar.Server.ServiceLibrary.ServiceLogic
{
    public class FindTasksLogic :
        RequestToResponseLogic<FindTasksRequest, FindTasksResponse>
    {
        private readonly ITasksRepository _tasksRepository;
        private readonly DtoMappingService _mappingService;

        public FindTasksLogic(ITasksRepository tasksRepository, DtoMappingService mappingService)
        {
            _tasksRepository = tasksRepository;
            _mappingService = mappingService;
        }

        public FindTasksResponse FindTasks(FindTasksRequest findTasksRequest)
        {
            TaskSpecificationDto taskSpecificationDto = findTasksRequest.TaskSpecificationDto;
            
			// there should not be any mapping from dto to domain 
            TaskSpecification taskSpecification = _mappingService.FromDto(taskSpecificationDto, findTasksRequest.CurrentUser);
        	IList<Task> specifiedTasks = new List<Task>();
        	var response = WithinTransactionDo(s =>
                       		{
                       			specifiedTasks = _tasksRepository.Find(taskSpecification);
                       		}).OnErrorSetMessage("Error finding tasks");
        	response.Tasks = _mappingService.ToDto(specifiedTasks);

        	return response;
        }
    }
  
}