using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Composite;
using TopCalendar.Utility.UI;

namespace TopCalendar.UI.Modules.TaskViewer
{
    public interface ITaskView : IViewForModel<ITaskView, TaskPresentationModel>, IActiveAware
    {

    }
}
