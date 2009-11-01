using System.Windows.Controls;

namespace TopCalendar.UI
{
    public interface IShellView
    {
        void Show();
        void Close();

        ShellPresentationModel Model { get; set; } 
    }
}