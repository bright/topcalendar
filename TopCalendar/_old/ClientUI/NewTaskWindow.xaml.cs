using System;
using System.Windows;
using ClientApp;
using ClientApp.Ninject;
using ClientApp.RemoteServerRef;

namespace ClientUI
{
    /// <summary>
    /// Interaction logic for NewTaskWindow.xaml
    /// </summary>
    public partial class NewTaskWindow : Window
    {
     
        public String NewEntryTitle
        {
            get { return _calendarEntry.Title; }
            set { _calendarEntry.Title = value;}
        }
        public String NewEntryDesc
        {
            get { return _calendarEntry.Desc; }
            set { _calendarEntry.Desc = value; }
        }
        public DateTime NewEntryDate
        {
            get { return _calendarEntry.DateTime; }
            set { _calendarEntry.DateTime = value; }
        }

        private readonly CalendarEntry _calendarEntry;

        public NewTaskWindow()
        {
            _calendarEntry = new CalendarEntry();
            NewEntryTitle = "Nowe zadanie";
            NewEntryDate = DateTime.Now;
            InitializeComponent();
        }

        /// <summary>
        /// Edycja
        /// </summary>
        public NewTaskWindow(CalendarEntry calendarEntry)
        {
            _calendarEntry = calendarEntry;
            InitializeComponent();
        }

        private void NewTaskCreateButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Validate())
            {
                MessageBox.Show("Pewne dane sa niepoprawne");
                return;
            }

            var newEntryCreator = Factory.Resolve<NewEntryCreator>();
            newEntryCreator.CalendarEntry = _calendarEntry;
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