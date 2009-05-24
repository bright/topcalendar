using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace ClientUI
{
    public class DragDestinationsHandler
    {
        public static readonly DragDestinationsHandler Instance = new DragDestinationsHandler();

        List<DragDestination> dragDestinations = new List<DragDestination>();

        public void RegisterDragDestination(ListBox listBox)
        {
            DragDestination dragDestination = new DragDestination();

            Window1 window = WpfHelper.FindAncestorOrSelf<Window1>(listBox);
            Visual mainGrid = window.MainGrid;

            Point leftTop = listBox.TransformToAncestor(mainGrid)
                     .Transform(new Point(0, 0));

            Point rightBottom = listBox.TransformToAncestor(mainGrid)
                            .Transform(new Point(listBox.Width, listBox.Height));

            dragDestination.Control = listBox;
            dragDestination.X1 = leftTop.X;
            dragDestination.Y1 = leftTop.Y;

            dragDestination.X2 = rightBottom.X;
            dragDestination.Y2 = rightBottom.Y;

            dragDestinations.Add(dragDestination);
        }

        public ListBox FindDragDestination(double x, double y)
        {
            var result = (from item in dragDestinations
                          where item.X1 < x
                             && item.X2 > x
                             && item.Y1 < y
                             && item.Y2 > y
                          select item.Control).SingleOrDefault();

            return result as ListBox;
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
