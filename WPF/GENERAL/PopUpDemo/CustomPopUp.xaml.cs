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

namespace PopUpDemo {
    /// <summary>
    /// Interaction logic for CustomPopUp.xaml
    /// </summary>
    public partial class CustomPopUp : UserControl {
        public CustomPopUp() {
            InitializeComponent();
        }

        public ImageSource Source {
            get { return (ImageSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(ImageSource), typeof(CustomPopUp), new PropertyMetadata(null));

        public ImageSource PopupImage {
            get { return (ImageSource)GetValue(PopupImageProperty); }
            set { SetValue(PopupImageProperty, value); }
        }

        public static readonly DependencyProperty PopupImageProperty =
            DependencyProperty.Register("PopupImage", typeof(ImageSource), typeof(CustomPopUp), new PropertyMetadata(null));

        public string PopupMessage {
            get { return (string)GetValue(PopupMessageProperty); }
            set { SetValue(PopupMessageProperty, value); }
        }

        public static readonly DependencyProperty PopupMessageProperty =
            DependencyProperty.Register("PopupMessage", typeof(string), typeof(CustomPopUp), new PropertyMetadata("This is a popup"));

        public string Text {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(CustomPopUp), new PropertyMetadata());

        public Brush Fill {
            get { return (Brush)GetValue(FillProperty); }
            set { SetValue(FillProperty, value); }
        }

        public static readonly DependencyProperty FillProperty =
            DependencyProperty.Register("Fill", typeof(Brush), typeof(CustomPopUp), new PropertyMetadata(Brushes.Orange));
    }
}
