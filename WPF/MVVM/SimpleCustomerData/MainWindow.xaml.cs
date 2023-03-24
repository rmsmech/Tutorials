using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SimpleCustomerData {

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        Shape _currentSelection = null;
        Point _startPoint = default(Point);
        bool _isDragging = false;

        public MainWindow() {
            InitializeComponent();
        }

        private void BtnDown(object sender, MouseButtonEventArgs e) {
            //todo:Add conditions to check if the pointer is above the canvas.
            //On down, create a shape immediately
            _isDragging = true;
            Shape mainshape = null;
            mainshape = new Rectangle() {
                Width = 10,
                Height = 10,
                Stroke= Brushes.Purple,
                StrokeThickness = 5,
            };
            cnvs.Children.Add(mainshape);
            var pt = e.GetPosition(cnvs);
            Canvas.SetLeft(mainshape, pt.X);
            Canvas.SetTop(mainshape, pt.Y);
            _currentSelection = mainshape;
            _startPoint = pt;
        }

        private void BtnUp(object sender, MouseButtonEventArgs e) {
            _isDragging =false ;
            var pt = e.GetPosition(cnvs);
            _currentSelection.Width = (pt.X - _startPoint.X) == 0 ? pt.X : (pt.X - _startPoint.X);
            _currentSelection.Height = (pt.Y - _startPoint.Y) == 0 ? pt.Y : (pt.Y - _startPoint.Y);
            _currentSelection = null;
            _startPoint = default(Point);
        }

        private void MouseMoving(object sender, MouseEventArgs e) {
            //When a mouse is moving over the cnavas, 
            if (_currentSelection == null || !_isDragging) return;
            var pt = e.GetPosition(cnvs);
            _currentSelection.Width = (pt.X - _startPoint.X) == 0 ? pt.X : (pt.X - _startPoint.X);
            _currentSelection.Height = (pt.Y - _startPoint.Y) == 0 ? pt.Y : (pt.Y - _startPoint.Y);
        }
    }
}