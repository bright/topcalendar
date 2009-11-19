using System.Windows;
using System.Windows.Controls;
using ClientApp.DateTimeExtensions;

namespace ClientUI
{
    /// <summary>
    /// Interaction logic for MonthViewer.xaml
    /// </summary>
    public partial class MonthViewer : UserControl
    {
        public MonthViewer()
        {
            InitializeComponent();
        }

        private void prevMonthBtn_Click(object sender, RoutedEventArgs e)
        {
            CurrentMonth.StartDate = CurrentMonth.StartDate.PrevMonth();
        }

        private void netxMonthBtn_Click(object sender, RoutedEventArgs e)
        {
            CurrentMonth.StartDate = CurrentMonth.StartDate.NextMonth();
        }
    }
}