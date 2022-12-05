using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFToggleButton {
    public class SimpleToggleButton : ToggleButton {

        private static CornerRadius _defaultCornerRadius = new CornerRadius(0.0);
        private static Brush _defaultOnColor = Brushes.MediumSeaGreen;
        private static Brush _defaultOffColor = Brushes.IndianRed;


        static SimpleToggleButton() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SimpleToggleButton), new FrameworkPropertyMetadata(typeof(SimpleToggleButton)));
        }

        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
        }
        public SimpleToggleButton() { }

        public CornerRadius CornerRadius {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(SimpleToggleButton), new PropertyMetadata(_defaultCornerRadius));

        public Brush ColorON {
            get { return (Brush)GetValue(ColorONProperty); }
            set { SetValue(ColorONProperty, value); }
        }

        public static readonly DependencyProperty ColorONProperty =
            DependencyProperty.Register(nameof(ColorON), typeof(Brush), typeof(SimpleToggleButton), new PropertyMetadata(_defaultOnColor));


        public Brush ColorOFF {
            get { return (Brush)GetValue(ColorOFFProperty); }
            set { SetValue(ColorOFFProperty, value); }
        }

        public static readonly DependencyProperty ColorOFFProperty =
            DependencyProperty.Register(nameof(ColorOFF), typeof(Brush), typeof(SimpleToggleButton), new PropertyMetadata(_defaultOffColor));

        public string LabelON {
            get { return (string)GetValue(LabelONProperty); }
            set { SetValue(LabelONProperty, value); }
        }

        public static readonly DependencyProperty LabelONProperty =
            DependencyProperty.Register(nameof(LabelON), typeof(string), typeof(SimpleToggleButton), new PropertyMetadata("ON"));

        public string LabelOFF {
            get { return (string)GetValue(LabelOFFProperty); }
            set { SetValue(LabelOFFProperty, value); }
        }

        public static readonly DependencyProperty LabelOFFProperty =
            DependencyProperty.Register(nameof(LabelOFF), typeof(string), typeof(SimpleToggleButton), new PropertyMetadata("OFF"));

        public double SwitchWidth {
            get { return (double)GetValue(SwitchWidthProperty); }
            set { SetValue(SwitchWidthProperty, value); }
        }

        public static readonly DependencyProperty SwitchWidthProperty =
            DependencyProperty.Register(nameof(SwitchWidth), typeof(double), typeof(SimpleToggleButton), new PropertyMetadata(40.0));

        public bool DisplayText {
            get { return (bool)GetValue(DisplayTextProperty); }
            set { SetValue(DisplayTextProperty, value); }
        }

        public static readonly DependencyProperty DisplayTextProperty =
            DependencyProperty.Register(nameof(DisplayText), typeof(bool), typeof(SimpleToggleButton), new PropertyMetadata(true));
    }
}
