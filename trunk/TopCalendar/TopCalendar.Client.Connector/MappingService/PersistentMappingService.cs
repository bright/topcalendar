#region

using System.Collections.Generic;
using AutoMapper;
using TopCalendar.Client.Connector.TopCalendarCommunicationService;
using TopCalendar.Client.DataModel;

#endregion

namespace TopCalendar.Client.Connector.MappingService
{
    /// <summary>
    /// Odpowiada za mapowanie obiektow domeny na dto
    /// i odwrotnie.
    /// 
    /// Obiekt dto w przeciwienstwie do obiektu domeny 
    /// posiada pole Id
    /// 
    /// Mapowanie z dto na obiekt domeny powoduje utrate 
    /// pola Id, stad potrzeba wprowadzenia Dictionary,
    /// ktore kazdemu obiektowi domeny przyporzadkowuje Dto.
    /// 
    /// </summary>
    public class PersistentMappingService : IMappingService
    {
        private readonly Dictionary<Task, TaskDto> _tasksBusinessToDto = new Dictionary<Task, TaskDto>();
        private readonly Dictionary<TaskDto, Task> _tasksDtoToBusiness = new Dictionary<TaskDto, Task>();

        public PersistentMappingService()
        {
            Mapper.CreateMap<Task, TaskDto>();
            Mapper.CreateMap<TaskDto, Task>();
        }

        /// <summary>
        /// Jezeli dany obiekt biznesowy zostal utworzony 
        /// na skutek przemapowania z DTO (swiadczy o tym slad w Dictionary)
        /// to zwraca to DTO.
        /// 
        /// W przeciwnym razie tworzony jest nowy DTO bez Id - slad nie jest nigdzie
        /// zapisywany.
        /// </summary>
        /// <param name="taskBusiness"></param>
        /// <returns></returns>
        public TaskDto ToDto(Task taskBusiness)
        {
            if (_tasksBusinessToDto.ContainsKey(taskBusiness))
            {
                return _tasksBusinessToDto[taskBusiness];
            }

            return Mapper.Map<Task, TaskDto>(taskBusiness);
        }

        /// <summary>
        /// Mapowanie z DTO na obiekt biznesowy powoduje
        /// zostawienie w Dictionary sladu wykonanego mappingu
        /// 
        /// Dzieki temu mozliwa jest konwersja w druga strone
        /// i uzyskanie obiektu z poprawnym Id
        /// 
        /// Jezeli "slad" juz istnial, to jest uaktualniony.
        /// </summary>
        /// <param name="taskDto"></param>
        /// <returns></returns>
        public Task FromDto(TaskDto taskDto)
        {
            if (!_tasksDtoToBusiness.ContainsKey(taskDto))
            {
                Task taskBusiness = Mapper.Map<TaskDto, Task>(taskDto);
                _tasksDtoToBusiness[taskDto] = taskBusiness;
                _tasksBusinessToDto[taskBusiness] = taskDto;
            }
            else
            {
                Mapper.Map(taskDto, _tasksDtoToBusiness[taskDto]);
            }

            return _tasksDtoToBusiness[taskDto];
        }

        public IList<Task> FromDto(IEnumerable<TaskDto> taskDtos)
        {
            IList<Task> resultsList = new List<Task>();

            foreach (TaskDto taskDto in taskDtos)
            {
                Task mappedTask = FromDto(taskDto);
                resultsList.Add(mappedTask);
            }

            return resultsList;
        }
    }

    public interface IMappingService
    {
        TaskDto ToDto(Task task);
        Task FromDto(TaskDto taskDto);
        IList<Task> FromDto(IEnumerable<TaskDto> taskDtos);
    }
}