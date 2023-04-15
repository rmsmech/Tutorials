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
using Microsoft.Web.WebView2.Core;

namespace AmazonScraping {
    /// <summary>
    /// Interaction logic for ScrapWindow.xaml
    /// </summary>
    public partial class ScrapWindow : Window {
        Uri _url;
        private ScrapWindow() {
            InitializeComponent();
        }

        public ScrapWindow(Uri webURL) : this() {
            _url = webURL;
            browser.Source = _url;
        }
    }
}
