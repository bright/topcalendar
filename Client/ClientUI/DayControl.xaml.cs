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

namespace ClientUI
{
    /// <summary>
    /// Interaction logic for DayControl.xaml
    /// </summary>
    public partial class DayControl : UserControl
    {
        public DateTime Date { get; set; }

        public List<CalendarEntry> EntryList { get; set; }

        public DayControl()
        {
            Date = DateTime.Now;
            InitializeComponent();
        }

        public DayControl(DateTime dt) {
            Date = dt;
            InitializeComponent();
        }
    }
}
