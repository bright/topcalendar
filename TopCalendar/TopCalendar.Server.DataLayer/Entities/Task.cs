using System;

namespace TopCalendar.Server.DataLayer.Entities
{
    public class Task
    {
        public virtual int Id { get; private set; }

        public virtual string Name { get; set; }

        public virtual DateTime StartAt { get; set; }

        public virtual DateTime FinishAt { get; set; }

        public virtual string Description { get; set; }

        public virtual User User { get; set; }
    }
}