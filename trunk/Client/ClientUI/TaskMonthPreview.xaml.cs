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

        public ListBox lastHighlighted;

        private DragAndDropService dragAndDropService;

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


        private TaskMonthPreview dragPhantom;
        private Point relativePoint;

        public TaskMonthPreview()
        {
            InitializeComponent();
        }

        void onDragDelta(object sender, DragDeltaEventArgs e)
        {

            Thickness t2 = new Thickness(0, 0, 0, 0);
            t2.Left = relativePoint.X + e.HorizontalChange;
            t2.Top = relativePoint.Y + e.VerticalChange;
            dragPhantom.Margin = t2;

            var dragDestination = DragDestinationsHandler.Instance.FindDragDestination(t2.Left, t2.Top);

            if (lastHighlighted != null)
            {
                lastHighlighted.Opacity = 1;
            }
            if (dragDestination != null)
            {
                dragDestination.Opacity = 0.3;
                lastHighlighted = dragDestination;
            }

        }

        void onDragStarted(object sender, DragStartedEventArgs e)
        {           
            Thumb senderThumb = (Thumb)sender;
            TaskMonthPreview tmp = (TaskMonthPreview)senderThumb.Parent;

            TaskMonthPreview copy = copyThumb(tmp);

            Window1 mainWindow = WpfHelper.FindAncestorOrSelf<Window1>(sender as Control);
            mainWindow.MainGrid.Children.Add(copy);

            this.dragPhantom = copy;

            this.dragAndDropService = new DragAndDropService();

            Point p = this.TransformToAncestor(mainWindow)
                           .Transform(new Point(0, 0));
            dragAndDropService.Source = DragDestinationsHandler.Instance.FindDragDestination(p.X, p.Y);
            dragAndDropService.Task = AttachedProperties.Task.GetTask(this);
        }

        TaskMonthPreview copyThumb(TaskMonthPreview src)
        {
            TaskMonthPreview dest = new TaskMonthPreview();
            dest.TaskTitle = src.TaskTitle;

            Window1 mainWindow = WpfHelper.FindAncestorOrSelf<Window1>(src);

            relativePoint = this.TransformToAncestor(mainWindow)
                              .Transform(new Point(0, 0));

            Point endPoint = this.TransformToAncestor(mainWindow)
                            .Transform(new Point(this.Width, this.Height));

            dest.HorizontalAlignment = HorizontalAlignment.Left;
            dest.VerticalAlignment = VerticalAlignment.Top;

            dest.Margin = new Thickness(relativePoint.X, relativePoint.Y, 0, 0);
            return dest;
        }

        void onDragCompleted(object sender, DragCompletedEventArgs e)
        {
            dragAndDropService.Destination = lastHighlighted;
            dragAndDropService.Move();
            if (lastHighlighted != null)
            {
                lastHighlighted.Opacity = 1;
            }

            Window1 mainWindow = WpfHelper.FindAncestorOrSelf<Window1>(dragPhantom);
            mainWindow.MainGrid.Children.Remove(dragPhantom);
            myThumb.Background = Brushes.Blue;
        }

        private void myThumb_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("niezle");
        }



    }
}
