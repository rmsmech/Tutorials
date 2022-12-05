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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Haley.Services;
using ThemeExternalTest;

namespace ThemeServiceTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IThemeService ts;
        public MainWindow()
        {
            ts = ThemeService.Singleton; //Get themeservice
            InitializeComponent();
            cbxTheme.SelectedItem = ts.StartupTheme;
        }

        private void cbxTheme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var newtheme = cbxTheme.SelectedItem as ComboBoxItem;
            ts.ChangeTheme(newtheme?.Content);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var extwndw = new ExternalWindwo();
            extwndw.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ts.ChangeTheme("Theme2");
        }
    }
}
