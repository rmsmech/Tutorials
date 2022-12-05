using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CustomControlButtons
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //Mainwindow
            //var _wndw1 = new MainWindow();
            //_wndw1.ShowDialog();

            //NewWindow
            var _wndw2 = new NewWindow();
            _wndw2.ShowDialog();
        }
    }
}
