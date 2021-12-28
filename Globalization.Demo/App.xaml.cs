using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Globalization;
using Haley.Utils;

namespace Globalization.Test
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        object _overrideCallBack(string key, string value, CultureInfo cultureInfo)
        {
            if (cultureInfo.TwoLetterISOLanguageName == "ta")
            {
                return "I don't have tamil translation yet.";
            }
            return value;
        }
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            LangUtils.Register();
            LangUtils.Register(typeof(External.ExternalControl).Assembly, "Globalization.External.Langresources.Language");
            ChangeCulture("en");
            var _wndw = new MainWindow();
            _wndw.ShowDialog();
        }

        public static void ChangeCulture(string code)
        {
            LangUtils.ChangeCulture(code);
            //CultureInfo _info = CultureInfo.CreateSpecificCulture(code); //ta - Tamil-India. de, fr
            //Thread.CurrentThread.CurrentCulture = _info;
            //Thread.CurrentThread.CurrentUICulture = _info;
        }
    }
}
