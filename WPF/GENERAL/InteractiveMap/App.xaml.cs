using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Haley.Rest;
using Haley.Models;

namespace InteractiveMap {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        private void Application_Startup(object sender, StartupEventArgs e) {
            Initialize();
            MainWindow mwndw = new MainWindow();
            mwndw.ShowDialog();
        }

        private void Initialize() {
            //Setup a rest api handler from Haley.
            var _bankClient = new FluentClient(@"http://api.worldbank.org/v2/");
            //Store this client in some place which can be accessed by other viewmodels.
            //Haley has inbuilt clientstore to store clients with a key.
            ClientStore.AddClient("worldbank", _bankClient);
        }
    }
}
