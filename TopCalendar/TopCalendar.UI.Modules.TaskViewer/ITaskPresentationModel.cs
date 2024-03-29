﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TopCalendar.Client.DataModel;
using TopCalendar.Utility.UI;

namespace TopCalendar.UI.Modules.TaskViewer
{
    public interface ITaskPresentationModel : IPresentationModelFor<ITaskView>
    {
        void ShowAddNewTaskView(DateTime? startDate);

        void ShowEditTaskView(Task taskToEdit);
    }
}
