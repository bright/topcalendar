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
    /// Interaction logic for MonthControl.xaml
    /// </summary>
    public partial class MonthControl : UserControl
    {

        public DateTime MonthStart { get; set; }

        public MonthControl()
        {            
            InitializeComponent();            
            MonthStart = monthStart(DateTime.Now);
            initMonthContent();
            initMonthGrid();
        }

        private void initMonthGrid()
        {
            int i = 0;
            foreach (DayControl dc in MonthContent)
            {
                dc.SetValue(Grid.RowProperty, i / 7);
                dc.SetValue(Grid.ColumnProperty, i % 7);
                MonthGrid.Children.Add(dc);
                ++i;
            }
        }
        

        private DateTime monthStart(DateTime dt) {
            DateTime date = new DateTime(dt.Year, dt.Month, 1);            
            return date;
        }

        private List<DayControl> MonthContent {get; set;}

        private void initMonthContent(){
            List<DayControl> dcList = new List<DayControl>();
            //TODO trzeba poprawic
            DateTime prevMonth = new DateTime(MonthStart.Year, MonthStart.Month - 1, 1);
            int n = (int)MonthStart.DayOfWeek;
            int prevDaysCount = DateTime.DaysInMonth(prevMonth.Year, prevMonth.Month);
            for (int i = 0; i < n; ++i) {
                DateTime dt = new DateTime(prevMonth.Year, prevMonth.Month, prevDaysCount - (n - i -1));
                dcList.Add(new DayControl(dt));
            }
            for (int i = 0; i < DateTime.DaysInMonth(MonthStart.Year, MonthStart.Month); ++i) {
                DateTime dt = new DateTime(MonthStart.Year, MonthStart.Month, i + 1);
                dcList.Add(new DayControl(dt));
            }
            //TODO trzeba poprawic
            DateTime nextMonth = new DateTime(MonthStart.Year, MonthStart.Month + 1, 1);
            for(int i = 1; dcList.Count < 42; ++i){
                DateTime dt = new DateTime(nextMonth.Year, nextMonth.Month, i);
                dcList.Add(new DayControl(dt));
            }
            MonthContent = dcList;
        }
    }
}
