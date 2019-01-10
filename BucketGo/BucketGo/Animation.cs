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
        private static DoubleAnimation appearAnimations;
        public const int WindowWidth = 634;
        public const int BlockWidth = 62;
        public const int step = 66;
        //path为从左到中间或从中间到右侧的距离
        public const int LeftPath = (WindowWidth - BlockWidth) / 2 - 20;
        public const int RightPath = (WindowWidth + BlockWidth) / 2 + 150;
        //duration为从左侧移动到中间或从中间移动到右侧的时间
        public const int AppearDuration = 500;
        public const int MoveDurationLeft = 2500;
        public const int MoveDurationRight = 3000;

        public static void Appear(Image i)
        {
            i.Visibility = Visibility.Visible;
        }
        public static void Disappear(Image i)
        {
            i.Visibility = Visibility.Hidden;
        }

        public static void LeftSlide(Image i, int startTime)
        {

            appearAnimations = new DoubleAnimation
            {
                BeginTime = new TimeSpan(0, 0, 0, 0, startTime),
                From = 0.0,
                To = 1.0,
                Duration = TimeSpan.FromMilliseconds(AppearDuration),
                FillBehavior = FillBehavior.HoldEnd
            };
            marginAnimations = new ThicknessAnimation
            {
                BeginTime = new TimeSpan(0, 0, 0, 0, startTime + AppearDuration),
                From = new Thickness(i.Margin.Left, i.Margin.Top, i.Margin.Right, i.Margin.Bottom),
                To = new Thickness(i.Margin.Left + LeftPath, i.Margin.Top, i.Margin.Right, i.Margin.Bottom),
                Duration = TimeSpan.FromMilliseconds(MoveDurationLeft),
                FillBehavior = FillBehavior.HoldEnd
            };
            i.BeginAnimation(Image.OpacityProperty, appearAnimations);
            i.BeginAnimation(Image.MarginProperty, marginAnimations);
            i.Visibility = Visibility.Visible;
        }

        public static void RightSlide(Image i, int startTime)
        {
            marginAnimations = new ThicknessAnimation
            {
                BeginTime = new TimeSpan(0, 0, 0, 0, startTime),
                From = new Thickness(i.Margin.Left, i.Margin.Top, i.Margin.Right, i.Margin.Bottom),
                To = new Thickness(i.Margin.Left + RightPath, i.Margin.Top, i.Margin.Right, i.Margin.Bottom),
                Duration = TimeSpan.FromMilliseconds(MoveDurationRight),
                FillBehavior = FillBehavior.HoldEnd
            };
            i.BeginAnimation(Image.MarginProperty, marginAnimations);
            i.Visibility = Visibility.Visible;
        }

        public static void LeftSlide_Rec(RectangleGeometry r, int startTime)
        {

        }
    }
}
