using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClientApp;
using System.Windows;
using ClientApp.RemoteServerRef;

namespace ClientUI.AttachedProperties
{
    public class Task
    {
        public static readonly DependencyProperty TaskProperty =
         DependencyProperty.RegisterAttached(
            "Task",
            typeof(BaseCalendarEntry),
            typeof(Task));

        public static BaseCalendarEntry GetTask(DependencyObject element)
        {
            return (BaseCalendarEntry)element.GetValue(TaskProperty);
        }

        public static void SetTask(DependencyObject element, BaseCalendarEntry value)
        {
            element.SetValue(TaskProperty, value);
        }

    }

    public class Date
    {
        public static readonly DependencyProperty DateProperty =
         DependencyProperty.RegisterAttached(
            "Date",
            typeof(DateTime),
            typeof(Date));

        public static DateTime GetDate(DependencyObject element)
        {
            return (DateTime)element.GetValue(DateProperty);
        }

        public static void SetDate(DependencyObject element, DateTime value)
        {
            element.SetValue(DateProperty, value);
        }

    }
}
