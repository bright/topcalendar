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
using ClientApp.Ninject;

namespace ClientUI
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void NewTaskButton_Click(object sender, RoutedEventArgs e)
        {
            NewTaskWindow newTaskWindow = new NewTaskWindow();
            newTaskWindow.Show();
        }

        private void MonthViewer_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window1_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            var dragDestinationsHandler = Factory.Resolve<IDragDestinationsHandler>();
            dragDestinationsHandler.RefreshAllDragDestinations();
            
        }
    }
}
