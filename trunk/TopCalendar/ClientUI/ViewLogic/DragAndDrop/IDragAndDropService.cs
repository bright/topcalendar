using System.Windows.Controls;
using ClientApp;
using ClientApp.RemoteServerRef;

namespace ClientUI
{
    public interface IDragAndDropService
    {
        ListBox Source { get; set; }
        ListBox Destination { get; set; }
        BaseCalendarEntry Task { get; set; }
        void Move();
    }
}