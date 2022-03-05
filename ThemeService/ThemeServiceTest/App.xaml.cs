using Haley.Abstractions;
using Haley.Enums;
using Haley.Events;
using Haley.Models;
using Haley.MVVM;
using Haley.Utils;
using Haley.Services;
using Haley.WPF;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ThemeExternalTest;
using System.Reflection;

namespace ThemeServiceTest
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IThemeService ts;
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            ts = ThemeService.Singleton; //Get themeservice
            ts.ErrorHandling = ExceptionHandling.ShowToast;
            //ts.ErrorHandling = ExceptionHandling.Throw;
            Register();
            //RegisterExternal();
            RegisterHaleyThemes();
            //ts.SetStartupTheme("Theme1");
            var _wnw = new MainWindow();
            _wnw.ShowDialog();
            //Theme1
            //Theme2
            //Theme3
        }

        private void RegisterHaleyThemes()
        {
            ts.SetupInternalTheme(HaleyThemeProvider.Prepare);
            var _internalgroup = new InternalThemeBuilder();
            _internalgroup
                .Add("Theme1", InternalThemeMode.Normal)
                .Add("Theme2", InternalThemeMode.Mild)
                .Add("Theme3", InternalThemeMode.Dark);
            ts.RegisterGroup(_internalgroup);
        }

        private void RegisterExternal()
        {
            EntryModule.RegisterThemes(ts);
        }

        void Register()
        {
            //InternalThemeBuilder
            //IndependentThemeBuilder
            //AssemblyThemeBuilder

            var _group1 = new IndependentThemeBuilder();
            _group1
                .Add("Theme1", new Uri(@"pack://application:,,,/ThemeExternalTest;component/Skins/Skin01.xaml"))
                .Add("Theme2", new Uri(@"pack://application:,,,/ThemeExternalTest;component/Skins/Skin02.xaml"))
                .Add("Theme3", new Uri(@"pack://application:,,,/ThemeExternalTest;component/Skins/Skin03.xaml"));

            var _group2 = new AssemblyThemeBuilder();
            _group2
                .Add("Theme1", new Uri(@"pack://application:,,,/ThemeServiceTest;component/Skins/themegroup2/Style01.xaml"))
                .Add("Theme2", new Uri(@"pack://application:,,,/ThemeServiceTest;component/Skins/themegroup2/Style02.xaml"))
                .Add("Theme3", new Uri(@"pack://application:,,,/ThemeServiceTest;component/Skins/themegroup2/Style01.xaml"));

            ts.RegisterGroup(_group1);
            ts.RegisterGroup(_group2);
        }
    }
}
