using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WPFOrgChart {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        private void Application_Startup(object sender, StartupEventArgs e) {
            Initialize();
            //Finally call the window.
            MainWindow mwndw = new MainWindow();
            mwndw.ShowDialog();
        }

        void Initialize() {
            //to initialize services or classes or objects as needed.

        }
    }
}
