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
using System.Windows.Controls.Primitives;
using ClientApp;
using ClientApp.Ninject;
using ClientApp.RemoteServerRef;

namespace ClientUI
{
    /// <summary>
    /// Kontrolka odpowiedzialna za wyswietlanie pojedynczego zadania 
    /// na liscie zadan dla danego dnia w widoku miesiaca
    /// </summary>
    public partial class TaskMonthPreview : UserControl
    {

        public static DependencyProperty TaskTitleProperty = DependencyProperty.Register(
              "TaskTitle", typeof(String), typeof(TaskMonthPreview));

        public static DependencyProperty MonthViewerProperty = DependencyProperty.Register(
            "MonthViewer", typeof(MonthViewer), typeof(TaskMonthPreview));

        public String TaskTitle
        {
            get { return (string)(GetValue(TaskTitleProperty)); }
            set { SetValue(TaskTitleProperty, value); }
        }

        /// <summary>
        /// MonthViewer, ktory reprezentuje kalendarz w ramach, ktorego
        /// wyswietlany jest ten TaskMonthPreview
        /// 
        /// Tylko w obrebie jednego MonthViewera mozna przenosic zadanie
        /// </summary>
        public MonthViewer MonthViewer
        {
            get { return (MonthViewer)(GetValue(MonthViewerProperty)); }
            set { SetValue(MonthViewerProperty, value); }
        }

        public ListBox LastHighlighted;

        private IDragAndDropService _dragAndDropService;
        private TaskMonthPreview _dragPhantom;
        private Point _relativePoint;
        private readonly IDragDestinationsHandler _dragDestinationsHandler;

        public TaskMonthPreview()
        {
            InitializeComponent();
            try
            {
                _dragDestinationsHandler = Factory.Resolve<IDragDestinationsHandler>();
            } catch (Exception)
            {
            }

        }

        void onDragDelta(object sender, DragDeltaEventArgs e)
        {

            var t2 = new Thickness(0, 0, 0, 0)
                         {
                             Left = _relativePoint.X + e.HorizontalChange,
                             Top = _relativePoint.Y + e.VerticalChange
                         };
            _dragPhantom.Margin = t2;

            var dragDestination = _dragDestinationsHandler.FindDragDestination(t2.Left, t2.Top);

            if (LastHighlighted != null)
            {
                LastHighlighted.Opacity = 1;
            }
            if (dragDestination != null)
            {
                dragDestination.Opacity = 0.3;
                LastHighlighted = dragDestination;
            }

        }

        void onDragStarted(object sender, DragStartedEventArgs e)
        {           
            var senderThumb = (Thumb)sender;
            var tmp = (TaskMonthPreview)senderThumb.Parent;

            TaskMonthPreview copy = CopyThumb(tmp);

            var mainWindow = WpfHelper.FindAncestorOrSelf<Window1>(sender as Control);
            mainWindow.MainGrid.Children.Add(copy);

            _dragPhantom = copy;

            _dragAndDropService = Factory.Resolve<IDragAndDropService>();

            Point p = this.TransformToAncestor(mainWindow)
                           .Transform(new Point(0, 0));
            _dragAndDropService.Source = _dragDestinationsHandler.FindDragDestination(p.X, p.Y);
            _dragAndDropService.Task = AttachedProperties.Task.GetTask(this);
        }

        TaskMonthPreview CopyThumb(TaskMonthPreview src)
        {
            var dest = new TaskMonthPreview {TaskTitle = src.TaskTitle};

            var mainWindow = WpfHelper.FindAncestorOrSelf<Window1>(src);

            _relativePoint = TransformToAncestor(mainWindow).Transform(new Point(0, 0));

            dest.HorizontalAlignment = HorizontalAlignment.Left;
            dest.VerticalAlignment = VerticalAlignment.Top;

            dest.Margin = new Thickness(_relativePoint.X, _relativePoint.Y, 0, 0);
            return dest;
        }

        void onDragCompleted(object sender, DragCompletedEventArgs e)
        {
            if (LastHighlighted != null)
            {
                _dragAndDropService.Destination = LastHighlighted;
                _dragAndDropService.Move();
                if (LastHighlighted != null)
                {
                    LastHighlighted.Opacity = 1;
                }
            }

            var mainWindow = WpfHelper.FindAncestorOrSelf<Window1>(_dragPhantom);
            mainWindow.MainGrid.Children.Remove(_dragPhantom);
            myThumb.Background = Brushes.Blue;
        }

        private void onDoubleClick(object sender, MouseButtonEventArgs e)
        {
     //       var baseCalendarEntry = AttachedProperties.Task.GetTask(this);
      //      var newTaskWindow = new TaskWindow(new CalendarEntry(baseCalendarEntry));
       //     newTaskWindow.Show();
        }
      
    }
}
