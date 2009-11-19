using System;
using System.Windows;

using ClientApp;
using ClientApp.Ninject;
using ClientApp.RemoteServerRef;



namespace ClientUI
{
    /// <summary>
    /// Interaction logic for TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        /// <summary>
        /// wystarczy ustawić tą zmienną, żeby określić okno 
        /// do tworznia nowego wpisu w kalendarzu
        /// </summary>
        public bool CreateNewEntryWindow {

            set {
                if (value == true)
                {
                    NewTaskCreateButton.Visibility = Visibility.Visible;
                    TaskEditButton.Visibility = Visibility.Hidden;
                }
                else
                {
                    NewTaskCreateButton.Visibility = Visibility.Hidden;
                    TaskEditButton.Visibility = Visibility.Visible;
                }
            } 
        }

        /// <summary>
        /// Wystarczy wystawić tą zmienną, żeby określić czy, 
        /// że okno ma być przystosowane do edytowania zadania
        /// </summary>
        public bool EditEntryWindow {
            set {

                if (value == true)
                {
                    TaskEditButton.Visibility = Visibility.Visible;
                    NewTaskCreateButton.Visibility = Visibility.Hidden;
                }
                else
                {
                    TaskEditButton.Visibility = Visibility.Hidden;
                    NewTaskCreateButton.Visibility = Visibility.Visible;
                }
                
            }
        }


        protected CalendarEntry calendarEntry;

        public TaskWindow()
        {
            calendarEntry = new CalendarEntry();
            EntryTitle = "Nowe zadanie";
            EntryDate = DateTime.Now;
            InitializeComponent();
            CreateNewEntryWindow = true;
        }

        public TaskWindow(CalendarEntry e) {
            
            calendarEntry = e;
            InitializeComponent();
            EditEntryWindow = true;

        }

        public String EntryTitle {
            get {return calendarEntry.Title; }
            set { calendarEntry.Title = value; }
        }
        public String EntryDesc {
            get { return calendarEntry.Desc; }
            set { calendarEntry.Desc = value; }
        }
        public DateTime EntryDate {
            get { return calendarEntry.DateTime; }
            set { calendarEntry.DateTime = value; }
        }

        private void NewTaskCreateButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Validate())
            {
                MessageBox.Show("Pewne dane sa niepoprawne");
                return;
            }

            var newEntryCreator = Factory.Resolve<NewEntryCreator>();
            newEntryCreator.CalendarEntry = calendarEntry;
            newEntryCreator.Save();

            //todo: takie odswiezanie nie moze zostac :)

            var dayControlsService = Factory.Resolve<IDayControlsService>();
            dayControlsService.RefreshAll();

            Close();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e) {

            if (!Validate()) {
                MessageBox.Show("Niepoprawne dane.");
                    return ;
            }

            IServer server = Factory.Resolve<IServer>();
            server.Edit(calendarEntry);
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
            return !String.IsNullOrEmpty(EntryTitle);
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