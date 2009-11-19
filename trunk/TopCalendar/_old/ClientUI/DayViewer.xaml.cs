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

namespace ClientUI
{
    /// <summary>
    /// Interaction logic for DayViewer.xaml
    /// </summary>
    public partial class DayViewer : UserControl
    {

        private DayView day;

        public DayViewer()
        {
            InitializeComponent();

            initDay();
        }

        private void initDay(){

            day = new DayView(DateTime.Today);
            day.SetValue(Grid.ColumnProperty,1);
            dayGrid.Children.Add(day);
            
        }
    }
}
