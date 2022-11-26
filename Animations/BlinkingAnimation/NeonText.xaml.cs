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

namespace BlinkingAnimation {
    /// <summary>
    /// Interaction logic for NeonText.xaml
    /// </summary>
    public partial class NeonText : UserControl {
        static Color _defaultColor = (Color)ColorConverter.ConvertFromString("#47bdfc");
        public NeonText() {
            InitializeComponent();
        }

        public string Text {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(NeonText), new PropertyMetadata("Mouse Events"));

        public Color GlowColor {
            get { return (Color)GetValue(GlowColorProperty); }
            set { SetValue(GlowColorProperty, value); }
        }

        public static readonly DependencyProperty GlowColorProperty =
            DependencyProperty.Register("GlowColor", typeof(Color), typeof(NeonText), new PropertyMetadata(_defaultColor));

        public bool ActivateBlink {
            get { return (bool)GetValue(ActivateBlinkProperty); }
            set { SetValue(ActivateBlinkProperty, value); }
        }
        public static readonly DependencyProperty ActivateBlinkProperty =
            DependencyProperty.Register("ActivateBlink", typeof(bool), typeof(NeonText), new PropertyMetadata(false));
    }
}
