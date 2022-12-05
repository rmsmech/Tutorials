using Haley.Abstractions;
using Haley.Enums;
using Haley.Events;
using Haley.Models;
using Haley.MVVM;
using Haley.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace NotificationDemo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            ContainerRegistrations();
            MainWindow _wndw = new MainWindow();
            _wndw.ShowDialog();
        }

        private void ContainerRegistrations()
        {
             ContainerStore.Singleton.controls.register<DialogVM, InputTest01>(mode:RegisterMode.Transient);
        }
    }
}
