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
using ClientApp.Ninject;
using System.Collections.ObjectModel;

namespace ClientUI
{
    /// <summary>
    /// Interaction logic for EntriesList.xaml
    /// </summary>
    public partial class EntriesList : UserControl
    {
        private LocalServerBase serv;

        public EntriesList()
        {

            serv = DIFactory.Resolve<LocalServerBase>();

            InitializeComponent();

            serv.EntriesListChanged += delegate { entriesList.Items.Refresh();};
            
        }

        public IEnumerable<CalendarEntry> Entries
        {
            get { return serv;}
        }
    }
}
