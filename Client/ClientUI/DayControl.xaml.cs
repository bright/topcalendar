using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ClientApp;
using ClientApp.Ninject;

namespace ClientUI
{
    /// <summary>
    /// Interaction logic for DayControl.xaml
    /// </summary>
    public partial class DayControl : UserControl
    {
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
            EntriesForDayList.ItemsSource = server.GetTasksForDate(Date);
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
    }
}