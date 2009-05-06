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


using ClientApp.DateTimeExtensions;
using System.ComponentModel;

namespace ClientUI
{
    /// <summary>
    /// Interaction logic for MonthControl.xaml
    /// </summary>
    public partial class MonthControl : UserControl, INotifyPropertyChanged
    {
        private DateTime startDate;

        public DateTime StartDate
        {
            get {
                return startDate;
            } 
            set {
                startDate = value;
                initMonthContent();
                initMonthGrid();
                RaisePropertyChanged("StartDate");
            } 
        }

        public MonthControl()
        {            
            InitializeComponent();
            StartDate = DateTime.Now.MonthStart();
        }

        private void initMonthGrid()
        {
            MonthGrid.Children.Clear();
            int i = 0;
            foreach (DayControl dc in MonthContent)
            {
                dc.SetValue(Grid.RowProperty, i / 7);
                dc.SetValue(Grid.ColumnProperty, i % 7);
                MonthGrid.Children.Add(dc);
                ++i;
            }
        }

        private List<DayControl> MonthContent {get; set;}

        private void initMonthContent(){
            List<DayControl> dcList = new List<DayControl>();

            DateTime prevMonth = StartDate.PrevMonth();
            int n = (int)StartDate.DayOfWeek;
            int prevDaysCount = DateTime.DaysInMonth(prevMonth.Year, prevMonth.Month);
            for (int i = 1; i < n; ++i) {
                DateTime dt = new DateTime(prevMonth.Year, prevMonth.Month, prevDaysCount - (n - i -1));
                dcList.Add(new DayControl(dt));
            }
            for (int i = 0; i < DateTime.DaysInMonth(StartDate.Year, StartDate.Month); ++i) {
                DateTime dt = new DateTime(StartDate.Year, StartDate.Month, i + 1);
                dcList.Add(new DayControl(dt));
            }
            //TODO trzeba poprawic
            DateTime nextMonth = StartDate.NextMonth();
            for(int i = 1; dcList.Count < 42; ++i){
                DateTime dt = new DateTime(nextMonth.Year, nextMonth.Month, i);
                dcList.Add(new DayControl(dt));
            }
            MonthContent = dcList;
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #endregion

    }
}
