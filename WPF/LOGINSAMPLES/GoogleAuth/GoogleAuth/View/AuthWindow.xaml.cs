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
using System.Windows.Shapes;
using Haley.WPF.Controls;
using GoogleAuth.ViewModel;
using Haley.Models;

namespace GoogleAuth.View {
    /// <summary>
    /// Interaction logic for AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : PlainWindow {
        public AuthWindow() {
            var vm = new AuthVM();
            WindowObserver wObserver = new WindowObserver(this, vm);
            InitializeComponent();
            this.DataContext = vm;
        }
    }
}
