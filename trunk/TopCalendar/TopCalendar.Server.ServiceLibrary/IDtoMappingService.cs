using TopCalendar.Server.DataLayer.Entities;
using TopCalendar.Server.DataLayer.Specifications;
using TopCalendar.Server.ServiceLibrary.ServiceContract.DataContract;
using TopCalendar.Server.ServiceLibrary.ServiceContract.DataContract.Dto;

namespace TopCalendar.Server.ServiceLibrary
{
    public interface IDtoMappingService
    {
        TaskDto ToDto(Task task);
        Task FromDto(TaskDto taskDto);
        TaskSpecification FromDto(TaskSpecificationDto taskSpecificationDto, User user);
    }
}