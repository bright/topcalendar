using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ClientApp;
using ClientApp.Ninject;
using ClientApp.RemoteServerRef;

namespace ClientUI
{
    public class DragAndDropService : IDragAndDropService
    {
        public ListBox Source { get; set; }
        public ListBox Destination { get; set; }
        public CalendarEntry Task { get; set; }

        private readonly IDayControlsService _dayControlsService;

        public DragAndDropService(IDayControlsService dayControlsService)
        {
            _dayControlsService = dayControlsService;
        }

        public void Move()
        {
            var sourceElementDate = AttachedProperties.Date.GetDate(Source);
            var destinationElementDate = AttachedProperties.Date.GetDate(Destination);

            Task.DateTime = destinationElementDate;
            
            var server = Factory.Resolve<IServer>();
            Source.ItemsSource = server.GetTasksForDate(sourceElementDate.Day, sourceElementDate.Month, sourceElementDate.Year);
            Destination.ItemsSource = server.GetTasksForDate(destinationElementDate.Day, destinationElementDate.Month, destinationElementDate.Year);

            _dayControlsService.RefreshAll();
        }

    }
}
