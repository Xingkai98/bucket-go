using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
namespace BucketGo
{
    public class Animation
    {
        private static ThicknessAnimation marginAnimations;
        public const int step = 66;
        public const int path = 100;
        public const double duration = 2.0;
        public static void MoveRight(Image i, int startTime)
        {
            marginAnimations = new ThicknessAnimation
            {
                BeginTime = new TimeSpan(0, 0, startTime),
                From = new Thickness(i.Margin.Left, i.Margin.Top, i.Margin.Right, i.Margin.Bottom),
                To = new Thickness(i.Margin.Left + path, i.Margin.Top, i.Margin.Right, i.Margin.Bottom),
                Duration = TimeSpan.FromSeconds(duration),
                FillBehavior = FillBehavior.HoldEnd
            };

            i.BeginAnimation(Image.MarginProperty, marginAnimations);
            i.Visibility = Visibility.Visible;
            
        }
    }
}
