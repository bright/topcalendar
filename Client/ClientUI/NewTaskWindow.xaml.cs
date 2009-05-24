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
            NewEntryTitle = "Nowe zadanie";
            NewEntryDate = DateTime.Now;
            InitializeComponent();
        }
     
        private void NewTaskCreateButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Validate())
            {
                MessageBox.Show("Pewne dane sa niepoprawne");
                return;
            }
            
            CalendarEntry calendarEntry = new CalendarEntry();
            calendarEntry.Title = this.NewEntryTitle;
            calendarEntry.Desc = this.NewEntryDesc;
            calendarEntry.DateTime = this.NewEntryDate;
            
            NewEntryCreator newEntryCreator = DIFactory.Resolve<NewEntryCreator>();
            newEntryCreator.CalendarEntry = calendarEntry;
            newEntryCreator.Save();

            //todo: takie odswiezanie nie moze zostac :)
            DayControlsService.Instance.RefreshAll();

            this.Close();
        }


        // todo: ta walidacja jest oczywiscie do calkowitego przerobienia
        // mysle, ze warto zastanowic sie nad taka walidacja jaka Manus pokazywal na zajeciach
        private bool Validate()
        {
            return ValidateTitle() && ValidateDesc() && ValidateDate();
        }

        private bool ValidateTitle()
        {
            return !String.IsNullOrEmpty(NewEntryTitle);
        }

        private bool ValidateDesc()
        {
            return true;
        }

        private bool ValidateDate()
        {
           // return this.NewEntryDate.ToBinary() > 0;
            return true;
        }
    }
}
