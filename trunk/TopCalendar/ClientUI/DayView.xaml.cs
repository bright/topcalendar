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
using ClientApp;
using ClientApp.Ninject;
using ClientApp.RemoteServerRef;
using ClientUI.Helpers;

namespace ClientUI
{
    /// <summary>
    /// Interaction logic for DayView.xaml
    /// </summary>
    public partial class DayView : UserControl
    {

        private DateTime today;
        public string DayOfWeek { get { return BasicHelper.DayOfWeek((int)today.DayOfWeek); } set { ;} }

        public DayView(DateTime dateTime)
        {
            today = dateTime;
            InitializeComponent();

            initDayContent();

            initDayGrid();
        }

        private List<TaskDayPreview> dayContent;
       
        // pobiera dane , ustawia zmienna dayContent
        private void initDayContent() {

            var server = Factory.Resolve<IServer>();
            // data z dokładnościa do dnia, bez konkretnej godziny
            List<BaseCalendarEntry> entries = server.GetTasksForDate(today.Day, today.Month, today.Year);

            dayContent = new List<TaskDayPreview>();

            foreach (BaseCalendarEntry e in entries) {

                dayContent.Add(new TaskDayPreview((CalendarEntry)e));

            }

        }

        private void initDayGrid() {

            // w zaleznosci od godziny zadania , task umieszczany w odpowiednim gridzie 
            //DayGrid.Children.Clear();

            foreach (TaskDayPreview task in dayContent) {

                task.SetValue(Grid.RowProperty,task.Hour + 2);

                DayGrid.Children.Add(task);

            }
        
        }
    }
}
