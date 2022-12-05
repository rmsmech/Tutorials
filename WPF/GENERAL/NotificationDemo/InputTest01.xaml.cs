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

namespace NotificationDemo
{
    /// <summary>
    /// Interaction logic for InputTest01.xaml
    /// </summary>
    public partial class InputTest01 : UserControl , IHaleyControl 
    {
        public InputTest01()
        {
            InitializeComponent();
        }
        
    }
}
