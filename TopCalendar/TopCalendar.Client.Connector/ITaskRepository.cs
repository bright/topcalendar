using System;
using System.Collections.Generic;
using TopCalendar.Client.DataModel;
using TopCalendar.Utility;

namespace TopCalendar.Client.Connector
{
    public interface ITaskRepository
    {
        IList<Task> GetTasksBetweenDates(DateTimeRange dateTimeRange);
        bool UpdateTask(Task task);
        bool AddTask(Task task);
    }
}