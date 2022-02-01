using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Haley.Abstractions;
using Haley.Enums;
using Haley.Events;
using Haley.IOC;
using Haley.Models;
using Haley.MVVM;
using Haley.Utils;
using LoginControl;
using LoginControl.Controls;


namespace MainAppSample
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IDialogService _ds;

        protected override void OnStartup(StartupEventArgs e)
        {
            _ds = ContainerStore.Singleton.DI.Resolve<IDialogService>();
            Entry.Initialize(ContainerStore.Singleton.GetFactory());
            base.OnStartup(e);
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var _actualmainwindow = new MainWindow(); //remmeber to dispose at end.
            int maxtries = 5;
            int trycount = 0;
            while (!CredentialHolder.Singleton.IsAuthenticated)
            {
                var _result = ContainerStore.Singleton.Windows.ShowDialog("authmainWindow");
                if (_result.HasValue && _result.Value)
                {
                    CredentialHolder.Singleton.IsAuthenticated = true; 
                    break;
                }
               
                trycount++; //add one count.
                if (trycount >= maxtries) break;
            }

            if (CredentialHolder.Singleton.IsAuthenticated)
            {
                //Show the main window (may be add other validations for feature control)
                _actualmainwindow.ShowDialog();
            }
            else
            {
                _ds?.Error("Authentication Failure", "Unable to authenticate. Application will close now.");
                if (Application.Current.MainWindow != null)
                {
                    Application.Current.MainWindow.Close();
                }
            }
        }

        private void ContainerRegistration()
        {

        }
    }
}
