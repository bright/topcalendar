using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


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
