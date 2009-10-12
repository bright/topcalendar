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

using ClientApp;

namespace ClientUI
{
    /// <summary>
    /// Interaction logic for CalendarEntryEditor.xaml
    /// </summary>
    public partial class CalendarEntryEditor : Window
    {

        public CalendarEntry Entry { get; set; }    

        public CalendarEntryEditor()
        {
            Entry = new CalendarEntry() {DateTime = DateTime.Now, Title = "tytuł", Desc = "opis"};
            InitializeComponent();            
        }

        public CalendarEntryEditor(CalendarEntry e)
        {
            Entry = e;
            InitializeComponent();            
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        
     
    }
}
