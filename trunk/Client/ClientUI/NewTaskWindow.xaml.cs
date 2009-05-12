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

        public String NewEntryTitle { get; set; }
        public String NewEntryDesc { get; set; }
        public DateTime NewEntryDate { get; set; }

        public NewTaskWindow()
        {
            InitializeComponent();


        }

     

        private void NewTaskCreateButton_Click(object sender, RoutedEventArgs e)
        {
            CalendarEntry calendarEntry = new CalendarEntry();
            calendarEntry.Title = this.TaskTitleTextBox.Text;

            NewEntryCreator newEntryCreator = DIFactory.Resolve<NewEntryCreator>();
            newEntryCreator.CalendarEntry = calendarEntry;
            newEntryCreator.Save();

            this.Close();
        }

        private bool Validate()
        {
            return true;
        }

        private bool ValidateTitle()
        {
            return true;
        }

        private bool ValidateDesc()
        {
            return true;
        }

        private bool validateDate()
        {
            return true;
        }
    }
}
