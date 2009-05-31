using System.Windows.Controls;
using ClientApp;

namespace ClientUI
{
    public interface IDragAndDropService
    {
        ListBox Source { get; set; }
        ListBox Destination { get; set; }
        CalendarEntry Task { get; set; }
        void Move();
    }
}