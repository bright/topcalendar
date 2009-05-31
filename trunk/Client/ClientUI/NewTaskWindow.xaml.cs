using System;
using System.Windows;
using ClientApp;
using ClientApp.Ninject;

namespace ClientUI
{
    /// <summary>
    /// Interaction logic for NewTaskWindow.xaml
    /// </summary>
    public partial class NewTaskWindow : Window
    {

        public NewTaskWindow()
        {
            NewEntryTitle = "Nowe zadanie";
            NewEntryDate = DateTime.Now;
            InitializeComponent();
        }

        public String NewEntryTitle { get; set; }
        public String NewEntryDesc { get; set; }
        public DateTime NewEntryDate { get; set; }

        private void NewTaskCreateButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Validate())
            {
                MessageBox.Show("Pewne dane sa niepoprawne");
                return;
            }

            var calendarEntry = new CalendarEntry();
            calendarEntry.Title = NewEntryTitle;
            calendarEntry.Desc = NewEntryDesc;
            calendarEntry.DateTime = NewEntryDate;

            var newEntryCreator = Factory.Resolve<NewEntryCreator>();
            newEntryCreator.CalendarEntry = calendarEntry;
            newEntryCreator.Save();

            //todo: takie odswiezanie nie moze zostac :)

            var dayControlsService = Factory.Resolve<IDayControlsService>();
            dayControlsService.RefreshAll();

            Close();
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