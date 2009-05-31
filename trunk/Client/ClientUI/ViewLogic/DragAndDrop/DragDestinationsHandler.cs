using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace ClientUI
{
    public class DragDestinationsHandler : IDragDestinationsHandler
    {

        readonly List<DragDestination> _dragDestinations = new List<DragDestination>();

        public void RegisterDragDestination(ListBox listBox)
        {
            try
            {
                var dragDestination = new DragDestination {Control = listBox};
                _dragDestinations.Add(dragDestination);

                RefreshDragDestination(dragDestination);

            } catch (Exception){}
        }

        /// <summary>
        /// Odswieza (oblicza na nowo wierzcholki) kontrolke zdefinio
        /// </summary>
        /// <param name="dragDestination"></param>
        private static void RefreshDragDestination(DragDestination dragDestination)
        {
            var control = dragDestination.Control;
            var window = WpfHelper.FindAncestorOrSelf<Window1>(control);
            Visual mainGrid = window.MainGrid;

            var leftTop = control.TransformToAncestor(mainGrid)
                .Transform(new Point(0, 0));

            var rightBottom = control.TransformToAncestor(mainGrid)
                .Transform(new Point(control.ActualWidth, control.ActualHeight));
            
            dragDestination.X1 = leftTop.X;
            dragDestination.Y1 = leftTop.Y;

            dragDestination.X2 = rightBottom.X;
            dragDestination.Y2 = rightBottom.Y;

        }

        public void RefreshAllDragDestinations()
        {
            _dragDestinations.ForEach(RefreshDragDestination);
        }

        public void ClearDragDestinations()
        {
            _dragDestinations.Clear();
        }

        public ListBox FindDragDestination(double x, double y)
        {
            var result = (from item in _dragDestinations
                          where item.X1 < x
                             && item.X2 > x
                             && item.Y1 < y
                             && item.Y2 > y
                          select item.Control).SingleOrDefault();

            return result;
        }
    }


    class DragDestination
    {
        public double X1 { get; set; }
        public double Y1 { get; set; }

        public double X2 { get; set; }
        public double Y2 { get; set; }

        public ListBox Control { get; set; }

    }

}
