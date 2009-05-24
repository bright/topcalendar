using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ClientApp;
using ClientApp.Ninject;

namespace ClientUI
{
    public class DragAndDropService
    {
        public ListBox Source { get; set; }
        public ListBox Destination { get; set; }
        public CalendarEntry Task { get; set; }

        public void Move()
        {
            DateTime sourceElementDate = AttachedProperties.Date.GetDate(Source);
            DateTime destinationElementDate = AttachedProperties.Date.GetDate(Destination);

            Task.DateTime = destinationElementDate;
            
            IServer server = DIFactory.Resolve<IServer>();
            Source.ItemsSource = server.GetTasksForDate(sourceElementDate);
            Destination.ItemsSource = server.GetTasksForDate(destinationElementDate);

            DayControlsService.Instance.RefreshAll();
        }

    }
}
