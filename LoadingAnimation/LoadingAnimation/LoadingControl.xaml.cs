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
using System.Windows.Media.Effects;

namespace LoadingAnimation {
    /// <summary>
    /// Interaction logic for LoadingControl.xaml
    /// </summary>
    public partial class LoadingControl : UserControl {
        private Storyboard loadingSB = new Storyboard();
        #region Properties
        public double TotalDuration { get; set; }
        public bool IgnoreSizeAnimation { get; set; }
        public bool IgnoreOpacityAnimation { get; set; }
        public int MaxSteps { get; set; }
        private int MaxPosition = 12; //From 0 = 12
        private string _loadingText;
        public string LoadingText {
            get { return _loadingText; }
            set { 
                _loadingText = value;
                this.loadingMsg.Text = LoadingText;
            }
        }



        #endregion
        public LoadingControl() {
            InitializeComponent();
            loadingSB.RepeatBehavior = RepeatBehavior.Forever;
            TotalDuration = 4.0; //Duration runs for 4 seconds by default
            MaxSteps = 5;
            IgnoreOpacityAnimation = false;
            IgnoreSizeAnimation = false;
        }

        public void ApplyAction(Action<Ellipse,int> action) {
            try {
                //Since we know we have 12 ellipse.
                for (int position = 0; position <= MaxPosition; position++) {
                    string elname =GetTargetName(position);
                    var target = this.FindName(elname) as Ellipse;
                    if (target == null) continue;
                    try {
                        action.Invoke(target, position);
                    }
                    catch (Exception) {
                        continue; //So one action fail should not stop us.
                    }
                }
            }
            catch (Exception) {
               //Log if possible
            }
        }

        public void SetCircleTheme(Brush background,Brush borderbrush,double borderthickness) {
            ApplyAction((target, position) => {
                target.Fill = background;
                target.Stroke = borderbrush;
                target.StrokeThickness = borderthickness;
            });
        }
        public void SetCircleShadow(Color color , double radius =8.0) {
            ApplyAction((target, position) => {
                target.Effect = new DropShadowEffect() { ShadowDepth = 0.0, BlurRadius = radius, Color = color };
            });
        }
        public void TurnOffShadow() {
            ApplyAction((target, position) => {
                target.Effect = null;
            });
        }

        private string GetTargetName(int position) {
            return $@"el{position.ToString()}";
        }

        private void MainLoadingGrid_Loaded(object sender, RoutedEventArgs e) {
            //Run the animation on load.
            PrepareLoadingAnimation();
            //Set some message if required.
            loadingSB.Begin(this);
        }

        private void PrepareLoadingAnimation() {
            ApplyAction((target, position) => {
                //For each ellipse add an animation
                //Get the target opacity at different steps
                var targetName = GetTargetName(position); //It could be el0 or el2 or ....

                if (!IgnoreOpacityAnimation) {
                    //Animate Opacity
                    var targetOpacity = GetTargetOpacities(position); //For the target, different opacity values at different time frame
                    var animationOpacityFrames = CreateFrames(targetOpacity); //Convert our opacity to double keyframe
                    var opacityAnimation = CreateAnimation(animationOpacityFrames); //Apply them on an animation
                    Storyboard.SetTargetName(opacityAnimation, targetName); //Connect the actual target property with the animation.
                    Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath(Ellipse.OpacityProperty));
                    loadingSB.Children.Add(opacityAnimation);
                }
                
                if(!IgnoreSizeAnimation) {
                    //Animate Size
                    var targetSize = GetTargetSize(position);
                    var animationSizeFrames = CreateFrames(targetSize, 1.0, DoubleKFType.Discrete); //Convert our opacity to double keyframe

                    //HEIGHT
                    var HeightAnimation = CreateAnimation(animationSizeFrames); //Apply them on an animation
                    Storyboard.SetTargetName(HeightAnimation, targetName); //Connect the actual target property with the animation.
                    Storyboard.SetTargetProperty(HeightAnimation, new PropertyPath(Ellipse.HeightProperty));
                    loadingSB.Children.Add(HeightAnimation);

                    ////WIDTH
                    var widthAnimation = CreateAnimation(animationSizeFrames); //Apply them on an animation
                    Storyboard.SetTargetName(widthAnimation, targetName); //Connect the actual target property with the animation.
                    Storyboard.SetTargetProperty(widthAnimation, new PropertyPath(Ellipse.WidthProperty));
                    loadingSB.Children.Add(widthAnimation);
                }
            });
        }

        private IEnumerable<DoubleKeyFrame> CreateFrames(Dictionary<int, (double opacity, KeyTime time)> targetValues,double defaultValue = 0.0,DoubleKFType kfType = DoubleKFType.Linear) {
            List<DoubleKeyFrame> result = new List<DoubleKeyFrame>();
            for (int i = 0; i <= MaxPosition; i++) {
                DoubleKeyFrame kf = null;
                switch(kfType) {
                    case DoubleKFType.Linear:
                        kf = new LinearDoubleKeyFrame() { Value = defaultValue, KeyTime = KeyTime.Uniform }; //When our value is at 0, let us divide the time and not pace it.
                        break;
                    case DoubleKFType.Discrete:
                        kf = new DiscreteDoubleKeyFrame() { Value = defaultValue, KeyTime = KeyTime.Uniform }; //When our value is at 0, let us divide the time and not pace it.
                        break;
                }
                
                if (targetValues.ContainsKey(i)) {
                    kf.Value = targetValues[i].opacity;
                    kf.KeyTime = targetValues[i].time;
                }
                result.Add(kf);
            }
            return result;
        }

        private DoubleAnimationBase CreateAnimation(IEnumerable<DoubleKeyFrame> keyFrames) {
            //
            try {
                Duration animationDuration = new Duration(TimeSpan.FromSeconds(TotalDuration));
                var animation = new DoubleAnimationUsingKeyFrames() { Duration = animationDuration };
                keyFrames.ToList().ForEach(p => animation.KeyFrames.Add(p));
                return animation;
            }
            catch (Exception) {
                return null;
            }
        }

        private KeyTime GetTime(int position) {
            return KeyTime.Paced;
        }
        private int FindPosition(int currentPos) {
            //Lets say, i'm at e11, position at 11. currentpos = 13.
            if (currentPos > MaxPosition) {
                return currentPos - MaxPosition - 1;
            }
            return currentPos;
        }
        private Dictionary<int,(double opacity,KeyTime time)> GetTargetOpacities(int position) {
            //We have 12 ellipse
            // 1 main ellipse where opacity is 1.0 and other trailing where we gradually decrease.
            Dictionary<int, (double, KeyTime)> result = new Dictionary<int, (double, KeyTime)>();

            //For position 7, my target is e6 (since we start at 0) with 1.0 opacity.
            //For position 8, my target is still e6 but with different opacity.

            //KeyTime property
            var time = GetTime(position); //For i (current target)
            var maxopacity = 1.0;
            var minopacity = 0.1;
            var segmentOpacity = (maxopacity - minopacity) / MaxSteps;
            for (int i = 0; i < MaxSteps; i++) {
                var currentOpacity = maxopacity - (i * segmentOpacity);
                result.Add(FindPosition(position + i), (currentOpacity, time));
            }

            //Lets say target is e7, then when i is at 7, we have e7 at 1.0 opacity.
            //when i is at 8, we have e7 at 0.75 opacity.
            //when i is at 9, we have e7 at 0.6 opacity.
            //When my position is one step ahead, my target opacity is little less
            return result;
        }

        //Now do the same for height animation
        private Dictionary<int, (double size, KeyTime time)> GetTargetSize(int position) {
            //We have 12 ellipse
            // 1 main ellipse where opacity is 1.0 and other trailing where we gradually decrease.
            Dictionary<int, (double, KeyTime)> result = new Dictionary<int, (double, KeyTime)>();

            //For position 7, my target is e6 (since we start at 0) with 1.0 opacity.
            //For position 8, my target is still e6 but with different opacity.

            //KeyTime property
            var time = GetTime(position); //For i (current target)
            var maxSize = 30.0;
            var minSize = 5.0;
            var segmentSize = (maxSize - minSize) / MaxSteps;
            for (int i = 0; i < MaxSteps; i++) {
                var currentSize = maxSize - (i * segmentSize);
                result.Add(FindPosition(position + i), (currentSize, time));
            }

            //Lets say target is e7, then when i is at 7, we have e7 at 1.0 opacity.
            //when i is at 8, we have e7 at 0.75 opacity.
            //when i is at 9, we have e7 at 0.6 opacity.
            //When my position is one step ahead, my target opacity is little less
            return result;
        }

    }
}
