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
    /// Interaction logic for EntryControl.xaml
    /// </summary>
    public partial class EntryControl : UserControl
    {
        public static readonly DependencyProperty CalendarEntryProperty
            = DependencyProperty.Register("Entry",typeof(CalendarEntry),typeof(EntryControl));

        public CalendarEntry Entry
        {
            get { return (CalendarEntry) GetValue(CalendarEntryProperty); }
            set { SetValue(CalendarEntryProperty,value);}
        }

        public EntryControl()
        {
            InitializeComponent();            
        }

        public EntryControl(CalendarEntry entry)
        {
            Entry = entry;
            InitializeComponent();
            MyTooltip.DataContext = Entry;
        }
    }
}
