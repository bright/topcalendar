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
using ClientApp.DateTimeExtensions;
using System.Collections.ObjectModel;
using ClientApp.RemoteServerRef;
using ClientApp.Ninject;

namespace ClientUI
{
    
    /// <summary>
    /// Interaction logic for WeekViewer.xaml
    /// </summary>
    ///     
    public partial class WeekViewer : UserControl
    {                
        public static readonly DependencyProperty WeekDateProperty
            = DependencyProperty.Register("WeekDate", typeof(DateTime), typeof(WeekViewer), new PropertyMetadata(new PropertyChangedCallback(WeekDateChanged)));

        public static readonly DependencyProperty WeekStartDateProperty
            = DependencyProperty.Register("WeekStartDate", typeof (DateTime), typeof (WeekViewer));

        public static readonly DependencyProperty WeekEndDateProperty
            = DependencyProperty.Register("WeekEndDate", typeof (DateTime), typeof (WeekViewer));
        
        /// <summary>
        /// data idetyfikująca dany tydzień
        /// </summary>
        public DateTime WeekDate
        {
            get { return (DateTime)GetValue(WeekDateProperty); }
            set { SetValue(WeekDateProperty,value);}
        }

        /// <summary>
        /// zwraca date będącą początkiem tygdonia wyswietlanego aktualnie, readonly
        /// </summary>
        public DateTime WeekStartDate
        {
            get { return (DateTime) GetValue(WeekStartDateProperty); }            
        }

        /// <summary>
        /// zwraca data będącą końcem tygodnia wyświetlanego aktualnie, readonly
        /// </summary>
        public DateTime WeekEndDate
        {
            get { return (DateTime) GetValue(WeekEndDateProperty); }
        }

        /// <summary>
        /// 7 dni tygodnia od poniedzialku do piątku w których znajduje sie <see cref="WeekDate"/>
        /// </summary>
        public ObservableCollection<DateTime> WeekDays
        {
            get; set;
        }

        private ObservableCollection<CalendarEntry> _entryList = new ObservableCollection<CalendarEntry>();

        public ObservableCollection<CalendarEntry> EntryList
        {
            get { return _entryList;  }
            set { _entryList = value; }
        }

        public WeekViewer()
        {
            _entryList.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(_entryList_CollectionChanged);
            InitEntries();                                 
            InitializeComponent();                        
            InitializeHoursLabels();
            
            WeekDate = DateTime.Now;   
        }

        void _entryList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {            
        }

        private void InitEntries()
        {
            IServer server = Factory.Resolve<IServer>();


        }
        public void UpdateWeekDays()
        {
            if( WeekDays == null )
                InitializeWeekDays();
            for (int i = 0; i < 7; ++i)
                WeekDays[i] = WeekStartDate.AddDays(i);
        }
        private void InitializeWeekDays()
        {
            if (WeekDays == null)
            {
                WeekDays = new ObservableCollection<DateTime>();
                for (int i = 0; i < 7; ++i)
                {
                    WeekDays.Add(DateTime.Now.AddDays(i));
                }
            }            
        }


        private static void WeekDateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var veekViewer = d as WeekViewer;
            if(veekViewer != null)
            {
                veekViewer.SetValue(WeekStartDateProperty, veekViewer.WeekDate.WeekStart());
                veekViewer.SetValue(WeekEndDateProperty, veekViewer.WeekDate.WeekEnd());
                veekViewer.UpdateWeekDays();
                DateTimeToColumnConverter.WeekStartDate = veekViewer.WeekStartDate;
            }

        }

        private void InitializeDateLabels()
        {
            var dt = WeekDate.WeekStart();
            var cdt = new DateTimeConverter();
            for(int i = 0; i< 7; ++i)
            {
                var dlabel = new Label();
                dlabel.Content = cdt.Convert(dt, typeof (string), null, null);
                dlabel.SetValue(Grid.RowProperty, 2);
                dlabel.SetValue(Grid.ColumnProperty, i + 1);
                dt = dt.AddDays(1);
                MainGrid.Children.Add(dlabel);
            }
        }

        private void InitializeHoursLabels()
        {
            DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            
            for(int i = 0; i< 24; ++i)
            {
                Label l = new Label();
                l.Content = dt.ToShortTimeString();
                l.SetValue(Grid.RowProperty, i);
                HoursTaskGrid.Children.Add(l);
                dt = dt.AddHours(1);
            }
        }

        private void PreviousWeekBtn_Click(object sender, RoutedEventArgs e)
        {
            WeekDate = WeekDate.AddDays(-7);
        }

        private void NextWeekBtn_Click(object sender, RoutedEventArgs e)
        {
            WeekDate = WeekDate.AddDays(7);
        }      

    }

    [ValueConversion(typeof(DateTime), typeof(int), ParameterType = typeof(DateTime))]
    internal class DateTimeToColumnConverter : IValueConverter
    {
        public static DateTime WeekStartDate = DateTime.Now;

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((DateTime)value).Subtract(WeekStartDate).Days + 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
