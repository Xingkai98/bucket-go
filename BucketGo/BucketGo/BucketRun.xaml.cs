using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
//using System.Timers.Timer;
using System.Threading;
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
using System.Windows.Threading;

//
namespace BucketGo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public const int MAX_PACKET = 100;
        public const int Maximum = 10;
        public const int RED = 101;
        public const int GREEN = 102;
        public int currentPacketId = 0;
        public int[] a = new int[10];
        public Packet[] p = new Packet[MAX_PACKET];
        public Data d = new Data();
        public Timeline myTimeLine = new Timeline();
        //public List<Image> image = new List<Image>();   
        public List<Image> bucketList = new List<Image>();
        public System.Timers.Timer timer;
        public long startMillis = 0;
        //已经进行了多少个100毫秒的周期
        public int tickCount = 0;
        //桶相关的数据
        public int Bucket_ = 0;
        public int Speed_ = 0;

        public MainWindow()
        {
            InitializeComponent();
            //本语句用于数据绑定，必须要有
            this.DataContext = d;
            bucketList.Add(littlebucket0);
            bucketList.Add(littlebucket1);
            bucketList.Add(littlebucket2);
            bucketList.Add(littlebucket3);
            bucketList.Add(littlebucket4);
            bucketList.Add(littlebucket5);
            bucketList.Add(littlebucket6);
            bucketList.Add(littlebucket7);
            bucketList.Add(littlebucket8);
            bucketList.Add(littlebucket9);
            bucketList.Add(littlebucket10);
            bucketList.Add(littlebucket11);
            bucketList.Add(littlebucket12);
            bucketList.Add(littlebucket13);
            bucketList.Add(littlebucket14);
            bucketList.Add(littlebucket15);
            bucketList.Add(littlebucket16);
            //开始只显示无令牌的情况
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                Animation.Appear(bucketList[0]);
                for (int i = 1; i <= 16; i++)
                {
                    Animation.Disappear(bucketList[i]);
                }
            }));
        }

        //计算出时间线并开始动画
        private void Button_Click_Start_Animation(object sender, RoutedEventArgs e)
        {

            timer = new System.Timers.Timer(100);
            //timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Enabled = true;
            timer.Elapsed += new System.Timers.ElapsedEventHandler(TimerUp);
            timer.Start();
            long currentTicks = DateTime.Now.Ticks;
            DateTime dtFrom = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            startMillis = (currentTicks - dtFrom.Ticks) / 10000;
        }

        private void BlockMove1()
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                Animation.LeftSlide(littleblock1, 0);
                Animation.RightSlide(littleblock_green1, Animation.AppearDuration + Animation.MoveDurationLeft);
            }));
        }
        private void BlockMove2()
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                Animation.LeftSlide(littleblock2, 0);
                Animation.RightSlide(littleblock_green2, Animation.AppearDuration + Animation.MoveDurationLeft);
            }));
        }
        private void BlockMove3()
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                Animation.LeftSlide(littleblock3, 0);
                Animation.RightSlide(littleblock_green3, Animation.AppearDuration + Animation.MoveDurationLeft);
            }));
        }
        private void BlockMove4()
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                Animation.LeftSlide(littleblock4, 0);
                Animation.RightSlide(littleblock_green4, Animation.AppearDuration + Animation.MoveDurationLeft);
            }));
        }
        private void BlockMove5()
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                Animation.LeftSlide(littleblock5, 0);
                Animation.RightSlide(littleblock_green5, Animation.AppearDuration + Animation.MoveDurationLeft);
            }));
        }

        private void updateBucketPerStep(int before, int after)
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                Animation.Disappear(bucketList[before]);
                Animation.Appear(bucketList[after]);
            }));
        }
        private void updateBucket(int tickCount)
        {

            if (myTimeLine.BucketShowTimeLine[tickCount] == myTimeLine.BucketShowTimeLine[tickCount - 1])
            {
                return;
            }
            else
            {
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    Animation.Appear(littlebucket16);
                }));
            }
        }

        //Timer的周期性操作函数
        private void TimerUp(object sender, System.Timers.ElapsedEventArgs e)
        {
            tickCount++;
            //先只显示前2.1秒。
            if (tickCount <= 21)
            {
                updateBucket(tickCount);
            }
            //if (tickCount % 10 == 0)
            //{
            //    this.Dispatcher.BeginInvoke(new Action(() =>
            //    {
            //        Animation.Appear(littlebucket16);
            //    }));
            //}
            //if (tickCount % 10 == 5)
            //{
            //    this.Dispatcher.BeginInvoke(new Action(() =>
            //    {
            //        Animation.Disappear(littlebucket16);
            //    }));
            //}
            if(tickCount == 10)
            {
                BlockMove1();
            }
            if(tickCount == 20)
            {
                BlockMove2();
            }
            if(tickCount == 30)
            {
                BlockMove3();
            }
            if(tickCount == 40)
            {
                BlockMove4();
            }
            if(tickCount == 50)
            {
                BlockMove5();
            }
            //long currentTicks = DateTime.Now.Ticks;
            //DateTime dtFrom = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            //long currentMillis = (currentTicks - dtFrom.Ticks) / 10000;
            //long timeElapsed = currentMillis - startMillis;

            //time.Text = tickCount.ToString();
        }
        private void Test()
        {
            while (true)
            {
                Thread.Sleep(1000);
                Animation.Appear(littlebucket16);
                Thread.Sleep(1000);
                Animation.Disappear(littlebucket16);
            }
        }
        //"添加分组"按钮函数，使用多线程处理
        private void Button_Click_Add_Packet(object sender, RoutedEventArgs e)
        {
            Thread thread = new Thread(updatePacket);
            thread.Start();
        }

        //更新桶信息
        private void Button_Click_Update_Bucket(object sender, RoutedEventArgs e)
        {
            if (d.Bucket == 0 || d.Speed == 0)
            {
                Xceed.Wpf.Toolkit.MessageBox m = new Xceed.Wpf.Toolkit.MessageBox();
                m.Caption = "请输入正确数据！";
                m.OkButtonContent = "确定";
                Xceed.Wpf.Toolkit.MessageBox.Show("请输入整数，例如1,2,3……");
            }
            else
            {
                Bucket_ = d.Bucket;
                Speed_ = d.Speed;
            }
        }

        //更新用户输入的新分组的信息
        private void updatePacket()
        {
            if(d.Size==0 || d.Time == 0.0)
            {
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    Xceed.Wpf.Toolkit.MessageBox.Show("请输入整数，例如1,2,3……");
                }));
            }
            else
            {
                int tempId = currentPacketId;
                //double tempTime = Convert.ToDouble(TimeInput.ToString());
                //int tempSize = Convert.ToInt32(SizeInput.ToString());
                int tempTime = d.Time;
                int tempSize = d.Size;
                Packet temp = new Packet(tempId, tempTime, tempSize);
                p[currentPacketId] = temp;

                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    //存储时，分组的Id是从0开始的，而展示时分组的Id是从1开始的
                    int tempShowId = currentPacketId + 1;
                    RightSideText.Text += tempShowId + "号分组到达时间：" + tempTime + "ms, 分组大小：" + tempSize + "字节。\n";
                    //这里若写在Dispatcher外部，则会先运行。
                    currentPacketId++;
                }));
            }   
        }

        private void Size_Input_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Time_Input_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        //测试动态生成图片（失败）
        private void Button_Click_Throw_Packet(object sender, RoutedEventArgs e)
        {
      
            // Create Image and set its width and height  
            Image dynamicImage = new Image();
            dynamicImage.Width = 62;
            dynamicImage.Height = 20;

            // Create a BitmapSource  
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri("Resources/littleblock.png", UriKind.RelativeOrAbsolute);
            bitmap.EndInit();

            // Set Image.Source  
            dynamicImage.Source = bitmap;

            // Add Image to Window  
            main.Children.Add(dynamicImage);
            dynamicImage.Visibility = Visibility.Visible;
        }
    }
}
