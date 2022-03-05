using Haley.Abstractions;
using Haley.Enums;
using Haley.Events;
using Haley.Models;
using Haley.MVVM;
using Haley.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThemeExternalTest
{
    public static class EntryModule
    {
        public static void RegisterThemes(IThemeService ts)
        {
            var _group1 = new AssemblyThemeBuilder();
            _group1
                .Add("Theme1", new Uri(@"pack://application:,,,/ThemeExternalTest;component/Skins/Skin01.xaml"))
                .Add("Theme2", new Uri(@"pack://application:,,,/ThemeExternalTest;component/Skins/Skin02.xaml"))
                .Add("Theme3", new Uri(@"pack://application:,,,/ThemeExternalTest;component/Skins/Skin03.xaml"));
            ts.RegisterGroup(_group1);
        }
    }
}
