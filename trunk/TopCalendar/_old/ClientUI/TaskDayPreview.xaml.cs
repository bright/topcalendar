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
using ClientApp.RemoteServerRef;

namespace ClientUI
{
    /// <summary>
    /// Interaction logic for TaskDayPreview.xaml
    /// </summary>
    public partial class TaskDayPreview : UserControl
    {

        public string TaskTitle { get { return entry.Title; } }

        public int Hour { get { return entry.DateTime.Hour; } }

        private CalendarEntry entry;
        public TaskDayPreview(CalendarEntry entry)
        {
            this.entry = entry;

            InitializeComponent();
        }
    }
}
