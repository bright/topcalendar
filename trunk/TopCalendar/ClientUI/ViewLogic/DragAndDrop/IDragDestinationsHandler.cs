using System.Windows.Controls;

namespace ClientUI
{
    public interface IDragDestinationsHandler
    {
        void RegisterDragDestination(ListBox listBox);
        ListBox FindDragDestination(double x, double y);
        void RefreshAllDragDestinations();
        void ClearDragDestinations();
    }
}