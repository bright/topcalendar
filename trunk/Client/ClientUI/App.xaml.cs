using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using ClientApp.Ninject;
using ClientUI.Ninject;

namespace ClientUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Factory.Load(new ClientUiDependencies());
            base.OnStartup(e);
        }
    }
}
