using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopCalendar.Client.DataModel
{
    public class Task
    {

        public string Name { get; set; }

        public DateTime StartAt { get; set; }

        public DateTime FinishAt { get; set; }

        public string Description { get; set; }
    }
}
