using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using GoogleAuth.Services;
using GoogleAuth.View;
using GoogleAuth.ViewModel;
using Haley.Models;

namespace GoogleAuth {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        Window dummy;
        private void Application_Startup(object sender, StartupEventArgs e) {
            InitializeApp();
        }

        async void InitializeApp() {
            dummy = new Window(); //Main window
            //Listener
            Globals.InitiateListener();
            await Task.Delay(1500);
            StartAuthorization();
            dummy.Close();
        }

        void StartAuthorization() {
            var awndw = new AuthWindow(); //No prior main window. So this will be Main window.
            var authResult = awndw.ShowDialog();
            if (authResult.HasValue && authResult.Value) {
                var hwndw = new HomeWindow();
                var mwndwResult = hwndw.ShowDialog();
                if (mwndwResult.HasValue && mwndwResult.Value) {
                    StartAuthorization();
                }
            }
        }
    }
}
