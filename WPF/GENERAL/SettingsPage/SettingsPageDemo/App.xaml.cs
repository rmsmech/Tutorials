using Haley.MVVM;
using SettingsPageDemo.Controls;
using SettingsPageDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SettingsPageDemo {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        private void Application_Startup(object sender, StartupEventArgs e) {
            ViewRegistrations();
            ContainerStore.Windows.ShowDialog<MainVM>();
        }

        void ViewRegistrations() {
            //Containerstore is present in Haley MVVM
            //Register Windows
            ContainerStore.Windows.Register<MainVM, MainWindow>();

            //Register Controls
            ContainerStore.Controls.RegisterWithKey<MainVM, CalculatorPage>(ViewEnum.HomeView);
            ContainerStore.Controls.RegisterWithKey<MainVM, SettingsPage>(ViewEnum.SettingsView);

            ContainerStore.Controls.RegisterWithKey<SettingsVM, GeneralSettings>(SettingsViewEnum.General);
            ContainerStore.Controls.RegisterWithKey<SettingsVM, CredentialSettings>(SettingsViewEnum.Credentials);
        }
    }
}
