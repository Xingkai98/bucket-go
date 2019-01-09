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
        //path为从左到中间以及从中间到右侧的距离
        public const int path = 200;
        //duration为从左侧移动到中间或从中间移动到右侧的时间
        public const int duration = 2000;
        //startTime为毫秒数，例如100表示100毫秒
        public static void LeftSlide(Image i, int startTime)
        {
            marginAnimations = new ThicknessAnimation
            {
                BeginTime = new TimeSpan(0, 0, 0, 0, startTime),
                From = new Thickness(i.Margin.Left, i.Margin.Top, i.Margin.Right, i.Margin.Bottom),
                To = new Thickness(i.Margin.Left + path, i.Margin.Top, i.Margin.Right, i.Margin.Bottom),
                Duration = TimeSpan.FromMilliseconds(duration),
                FillBehavior = FillBehavior.HoldEnd
            };

            i.BeginAnimation(Image.MarginProperty, marginAnimations);
            i.Visibility = Visibility.Visible;
            
        }
    }
}
