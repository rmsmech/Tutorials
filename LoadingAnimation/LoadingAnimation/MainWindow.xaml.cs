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
using System.Windows.Media.Animation;

namespace LoadingAnimation {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        UserControl loadingObj;
        public MainWindow() {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            double.TryParse(tbxSeconds.Text, out var duration);
            if (duration == 0.0) duration = 3.0;

            int.TryParse(tbxMaxStep.Text, out var maxSteps);
            if (maxSteps == 0) maxSteps = 4;

            loadingObj = new LoadingControl() {
                LoadingText = "Hello there, I'm loading",
                TotalDuration = duration,
                MaxSteps =maxSteps,
                IgnoreSizeAnimation = cbxIgnoreSize.IsChecked.Value,
                IgnoreOpacityAnimation = cbxIgnoreOpacity.IsChecked.Value
            };

            //newLoading.SetCircleTheme(Brushes.Pink, Brushes.Gray, 1.0);
            //newLoading.SetCircleShadow(Colors.Turquoise, 10);

            loadingContent.Content = loadingObj;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {
            //Stop the animation.
            loadingObj = null;
            loadingContent.Content = loadingObj;
        }
    }
}