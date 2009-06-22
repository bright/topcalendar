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

        private IServer server = Factory.Resolve<IServer>();

        private NewEntryCreator entryEditor = Factory.Resolve<NewEntryCreator>();

        public ObservableCollection<CalendarEntry> EntryList
        {
            get { return _entryList;  }
            set { _entryList = value; }
        }

        public WeekViewer()
        {
            _entryList.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(_entryList_CollectionChanged);            
            InitializeComponent();                        
            InitializeHoursLabels();            
            WeekDate = DateTime.Now;
            InitEntries();                                 
        }

        void _entryList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {            
            if (e.NewItems != null)
                HandleNewItems(e.NewItems);            
        }

        private void HandleNewItems(System.Collections.IList iList)
        {            
            var dateToColumn = new DateTimeToColumnConverter();
            var dateToRow = new DateTimeToRowConverter();
            foreach(CalendarEntry ce in iList)
            {
                EntryControl ec = new EntryControl(ce);
                ec.MouseRightButtonDown += new MouseButtonEventHandler(ec_MouseRightButtonDown);                
                ec.SetValue(Grid.ColumnProperty, dateToColumn.Convert(ce.DateTime,typeof(int),null,null));
                ec.SetValue(Grid.RowProperty, dateToRow.Convert(ce.DateTime,typeof(int),null,null));
                HoursTaskGrid.Children.Add(ec);
            }
        }

        void ec_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            EditEntry(((EntryControl)sender).Entry);
        }

        private void ClearEntryControls()
        {
            List<UIElement> list = new List<UIElement>();
            foreach(UIElement ui in HoursTaskGrid.Children)                
                if(ui is EntryControl)
                    list.Add(ui);
            foreach (var element in list)
            {
                HoursTaskGrid.Children.Remove(element);
            }
        }

        private void InitEntries()
        {  
            ClearEntryControls();
            _entryList.Clear();            
            foreach(CalendarEntry ce in server.GetTasksBetweenDates(WeekStartDate, WeekEndDate))
            {
                _entryList.Add(ce);
            }
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
            InitEntries();
        }

        private void NextWeekBtn_Click(object sender, RoutedEventArgs e)
        {
            WeekDate = WeekDate.AddDays(7);
            InitEntries();
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            CalendarEntryEditor cee = new CalendarEntryEditor();
            cee.ShowDialog();
            entryEditor.CalendarEntry = cee.Entry;
            entryEditor.Save();
            InitEntries();
        }      

        private void EditEntry(CalendarEntry entry)
        {
            CalendarEntryEditor cee = new CalendarEntryEditor(entry);
            cee.ShowDialog();
            entryEditor.CalendarEntry = cee.Entry;
            entryEditor.Save();
            InitEntries();
        }
    }

    [ValueConversion(typeof(DateTime), typeof(int))]
    internal class DateTimeToColumnConverter : IValueConverter
    {
        public static DateTime WeekStartDate = DateTime.Now;

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if( parameter == null) 
                return ((DateTime)value).Subtract(WeekStartDate).Days + 1;
            else
                return ((DateTime)value).Subtract((DateTime)parameter).Days + 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
    [ValueConversion(typeof(DateTime), typeof(int))]
    internal class DateTimeToRowConverter : IValueConverter
    {        

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((DateTime) value).Hour;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
