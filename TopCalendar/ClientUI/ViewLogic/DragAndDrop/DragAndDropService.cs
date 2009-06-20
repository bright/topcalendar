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
        public BaseCalendarEntry Task { get; set; }

        private readonly IDayControlsService _dayControlsService;
        private readonly IServer _server;

        public DragAndDropService(IDayControlsService dayControlsService, IServer server)
        {
            _dayControlsService = dayControlsService;
            _server = server;
        }

        public void Move()
        {
            var sourceElementDate = AttachedProperties.Date.GetDate(Source);
            var destinationElementDate = AttachedProperties.Date.GetDate(Destination);

            Task.DateTime = destinationElementDate;
                        
            Source.ItemsSource = _server.GetTasksForDate(sourceElementDate.Day, sourceElementDate.Month, sourceElementDate.Year);
            Destination.ItemsSource = _server.GetTasksForDate(destinationElementDate.Day, destinationElementDate.Month, destinationElementDate.Year);

            _server.Remove(Task);
            _server.Add(Task);

            _dayControlsService.RefreshAll();
            
        }


    }
}
