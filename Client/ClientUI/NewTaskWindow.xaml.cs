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
using System.Windows.Shapes;
using ClientApp.Ninject;
using ClientApp;

namespace ClientUI
{
    /// <summary>
    /// Interaction logic for NewTaskWindow.xaml
    /// </summary>
    public partial class NewTaskWindow : Window
    {
        public NewTaskWindow()
        {
            InitializeComponent();
        }

        private void NewTaskCreateButton_Click(object sender, RoutedEventArgs e)
        {
            CalendarEntry calendarEntry = new CalendarEntry();
            calendarEntry.Title = this.TaskTitleTextBox.Text;

            NewEntryCreator nec = DIFactory.Resolve<NewEntryCreator>();
            nec.CalendarEntry = calendarEntry;
            nec.Save();
            this.Close();
        }
    }
}
