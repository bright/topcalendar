#region

using System.Collections.Generic;
using AutoMapper;
using TopCalendar.Server.DataLayer.Entities;
using TopCalendar.Server.DataLayer.Specifications;
using TopCalendar.Server.ServiceLibrary.ServiceContract.DataContract;
using TopCalendar.Server.ServiceLibrary.ServiceContract.DataContract.Dto;

#endregion

namespace TopCalendar.Server.ServiceLibrary
{
    public class DtoMappingService : IDtoMappingService
    {
        public DtoMappingService()
        {
            Mapper.CreateMap<Task, TaskDto>();
            Mapper.CreateMap<TaskDto, Task>();
            Mapper.CreateMap<TaskSpecificationDto, TaskSpecification>()
				.ForMember(dst=> dst.User, opt=> opt.Ignore());
        }

        public TaskDto ToDto(Task task)
        {
            return Mapper.Map<Task, TaskDto>(task);
        }

        public IList<TaskDto> ToDto(IList<Task> task)
        {
            return Mapper.Map<IList<Task>, IList<TaskDto>>(task);
        }

        public Task FromDto(TaskDto taskDto)
        {
            return Mapper.Map<TaskDto, Task>(taskDto);
        }

        public TaskSpecification FromDto(TaskSpecificationDto taskSpecificationDto, User user)
        {
			TaskSpecification taskSpecification = new TaskSpecification(user);
            taskSpecification = Mapper.Map(taskSpecificationDto,taskSpecification);            
            return taskSpecification;
        }
    }
}