using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ClientApp;
using ClientApp.Ninject;
using ClientApp.RemoteServerRef;

namespace ClientUI
{
    /// <summary>
    /// Interaction logic for DayControl.xaml
    /// </summary>
    public partial class DayControl : UserControl
    {
        TaskWindow taskWindow;
        public DayControl()
        {
            Date = DateTime.Now;
            InitializeComponent();
        }

        public DayControl(DateTime dt)
        {
            Date = dt;
            InitializeComponent();
            Refresh();
        }

        public DateTime Date { get; set; }

        public List<CalendarEntry> EntryList { get; set; }

        public void Refresh()
        {
            var server = Factory.Resolve<IServer>();
            EntriesForDayList.ItemsSource = server.GetTasksForDate(Date.Day, Date.Month, Date.Year);
        }

        private void userControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var dayControlsService = Factory.Resolve<IDayControlsService>();
                dayControlsService.Register(this);

                var dragDestinationsHandler = Factory.Resolve<IDragDestinationsHandler>();
                dragDestinationsHandler.RegisterDragDestination(EntriesForDayList);
                AttachedProperties.Date.SetDate(EntriesForDayList, Date);
            }
            catch (Exception)
            {
            }
        }

        private void EntriesForDayList_MouseEnter(object sender, MouseEventArgs e)
        {
            var listBox = (ListBox) sender;            
            listBox.ToolTip = "(" + listBox.ActualHeight + ", " + listBox.ActualWidth + ")";
        }

        /// <summary>
        /// This event should edit calendar entry
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EntriesForDayList_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            CalendarEntry entry = (CalendarEntry)((BaseCalendarEntry)EntriesForDayList.SelectedValue);

            taskWindow = new TaskWindow(entry);
            taskWindow.Show();
            

        }
    }
}
