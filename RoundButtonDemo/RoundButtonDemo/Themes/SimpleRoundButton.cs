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

namespace RoundButtonDemo.Themes
{
    public class SimpleRoundButton : Button
    {
        private static CornerRadius _defaultCornerRadius = new CornerRadius(10.0);
        static SimpleRoundButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SimpleRoundButton), new FrameworkPropertyMetadata(typeof(SimpleRoundButton)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        //Since we are overriding a button, we already have all the properties that we need. We will merely over write the UI in a resource dictionary and we will be done.
        // We don't need to do anything els other than the new properties that we will add

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(SimpleRoundButton), new PropertyMetadata(_defaultCornerRadius));
    }
}
