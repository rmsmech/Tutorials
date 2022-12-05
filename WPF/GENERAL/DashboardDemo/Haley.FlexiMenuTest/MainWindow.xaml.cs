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
using Haley.Abstractions;
using Haley.Enums;
using Haley.Events;
using Haley.FlexiMenuTest.ViewModels;
using Haley.Models;
using Haley.MVVM;
using Haley.Utils;
using Haley.WPF.Controls;


namespace Haley.FlexiMenuTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : PlainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            //If datacontext of the views (controls) are not set explicitly, they take it from the parent.
            this.DataContext = new MainViewModel();
        }
    }
}
