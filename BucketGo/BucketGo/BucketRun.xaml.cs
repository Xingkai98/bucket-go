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
        public const int BLUE = 997;
        public const int RED = 998;
        public const int GREEN = 999;
        public int currentPacketId = 0;
        public int[] a = new int[10];
        public Packet[] p = new Packet[MAX_PACKET];
        public Data d = new Data();
        public Timeline myTimeLine = new Timeline();
        public token_bucket calculator = new token_bucket();
        //public List<Image> image = new List<Image>();   
        public List<Image> bucketList = new List<Image>();
        public List<Image> blockList = new List<Image>();
        public List<Image> redBlockList = new List<Image>();
        public List<Image> greenBlockList = new List<Image>();
        public System.Timers.Timer timer;
        public long startMillis = 0;
        //已经进行了多少个100毫秒的周期
        public int tickCount = 0;
        //三中颜色三种大小，九种状态每种八张图。记录已经使用到了第几个图，一共9*8=72个图
        //blueCount[2]代表蓝色大小二使用到了哪里
        public int[] blueCount = new int[4]{ 0, 1, 1, 1 };
        public int[] redCount = new int[4] { 0, 1, 1, 1 };
        public int[] greenCount = new int[4] { 0, 1, 1, 1 };
        //桶相关的数据
        public int Bucket_ = 0;
        public int Speed_ = 0;

        public MainWindow()
        {
            InitializeComponent();
            //本语句用于数据绑定，必须要有
            this.DataContext = d;
            //由于单个图片的变量名无法遍历，只能一个一个加，有些影响代码美观
            //桶
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
            //未标记的分组
            blockList.Add(littleblock1_1);
            blockList.Add(littleblock1_2);
            blockList.Add(littleblock1_3);
            blockList.Add(littleblock1_4);
            blockList.Add(littleblock1_5);
            blockList.Add(littleblock1_6);
            blockList.Add(littleblock1_7);
            blockList.Add(littleblock1_8);
            blockList.Add(littleblock2_1);
            blockList.Add(littleblock2_2);
            blockList.Add(littleblock2_3);
            blockList.Add(littleblock2_4);
            blockList.Add(littleblock2_5);
            blockList.Add(littleblock2_6);
            blockList.Add(littleblock2_7);
            blockList.Add(littleblock2_8);
            blockList.Add(littleblock3_1);
            blockList.Add(littleblock3_2);
            blockList.Add(littleblock3_3);
            blockList.Add(littleblock3_4);
            blockList.Add(littleblock3_5);
            blockList.Add(littleblock3_6);
            blockList.Add(littleblock3_7);
            blockList.Add(littleblock3_8);
            //标记为红色的分组
            redBlockList.Add(littleblock_red1_1);
            redBlockList.Add(littleblock_red1_2);
            redBlockList.Add(littleblock_red1_3);
            redBlockList.Add(littleblock_red1_4);
            redBlockList.Add(littleblock_red1_5);
            redBlockList.Add(littleblock_red1_6);
            redBlockList.Add(littleblock_red1_7);
            redBlockList.Add(littleblock_red1_8);
            redBlockList.Add(littleblock_red2_1);
            redBlockList.Add(littleblock_red2_2);
            redBlockList.Add(littleblock_red2_3);
            redBlockList.Add(littleblock_red2_4);
            redBlockList.Add(littleblock_red2_5);
            redBlockList.Add(littleblock_red2_6);
            redBlockList.Add(littleblock_red2_7);
            redBlockList.Add(littleblock_red2_8);
            redBlockList.Add(littleblock_red3_1);
            redBlockList.Add(littleblock_red3_2);
            redBlockList.Add(littleblock_red3_3);
            redBlockList.Add(littleblock_red3_4);
            redBlockList.Add(littleblock_red3_5);
            redBlockList.Add(littleblock_red3_6);
            redBlockList.Add(littleblock_red3_7);
            redBlockList.Add(littleblock_red3_8);
            //标记为绿色的分组
            greenBlockList.Add(littleblock_green1_1);
            greenBlockList.Add(littleblock_green1_2);
            greenBlockList.Add(littleblock_green1_3);
            greenBlockList.Add(littleblock_green1_4);
            greenBlockList.Add(littleblock_green1_5);
            greenBlockList.Add(littleblock_green1_6);
            greenBlockList.Add(littleblock_green1_7);
            greenBlockList.Add(littleblock_green1_8);
            greenBlockList.Add(littleblock_green2_1);
            greenBlockList.Add(littleblock_green2_2);
            greenBlockList.Add(littleblock_green2_3);
            greenBlockList.Add(littleblock_green2_4);
            greenBlockList.Add(littleblock_green2_5);
            greenBlockList.Add(littleblock_green2_6);
            greenBlockList.Add(littleblock_green2_7);
            greenBlockList.Add(littleblock_green2_8);
            greenBlockList.Add(littleblock_green3_1);
            greenBlockList.Add(littleblock_green3_2);
            greenBlockList.Add(littleblock_green3_3);
            greenBlockList.Add(littleblock_green3_4);
            greenBlockList.Add(littleblock_green3_5);
            greenBlockList.Add(littleblock_green3_6);
            greenBlockList.Add(littleblock_green3_7);
            greenBlockList.Add(littleblock_green3_8);
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
            //向calculator输入数据以供计算
            calculator.rate = Speed_;
            calculator.bucket_size = Bucket_;
            int maxTime = 0;
            for (int i = 0; i < currentPacketId; i++)
            {
                if (p[i].arrivalTime > maxTime)
                    maxTime = p[i].arrivalTime;
            }
            for (int i = 0; i < currentPacketId; i++)
            {
                calculator.packet_size[p[i].arrivalTime]=p[i].size;
            }
            maxTime++;
            calculator.calculate(maxTime);
            //还需要维护一下最大分组长度和最小分组长度，以便生成分组时选择大小
            int maxSize = 0;
            for(int i=0;i< currentPacketId; i++)
            {
                if (p[i].size > maxSize)
                    maxSize = p[i].size;
            }
            int minSize = maxSize;
            for (int i = 0; i < currentPacketId; i++)
            {
                if (p[i].size < minSize)
                    minSize = p[i].size;
            }
            //计算完毕，接下来根据时间线进行动画模拟
            for (int i = 0; i < maxTime; i++)
            {
                if (calculator.packet_size[i] > 0)
                {
                    //int temp = i;
                    //x秒进入桶，对应调用动画为10(x-1)下标
                    //颜色假设calculator中1为红色0为绿色则
                    //进行大小的翻译
                    //int translated_size = ((double)calculator.packet_size[i]- (double)minSize)/((double)maxSize - (double)minSize)*(double)2+(double)1
                    int temp = calculator.packet_size[i];
                    int translated_size = 0;
                    int gap = (maxSize - minSize)/3;
                    if(temp>=minSize && temp < minSize + gap)
                    {
                        translated_size = 1;
                    }
                    else if(temp >= minSize+gap && temp < minSize + 2 * gap)
                    {
                        translated_size = 2;
                    }
                    else
                    {
                        translated_size = 3;
                    }
                    if (calculator.color[i] == 1)
                    {
                        myTimeLine.PacketGoTimeLine[10 * (i - 1)] = GREEN;
                        myTimeLine.PacketGoTimeLine_Size[10 * (i - 1)] = translated_size;
                    }
                    else
                    {
                        myTimeLine.PacketGoTimeLine[10 * (i - 1)] = RED;
                        myTimeLine.PacketGoTimeLine_Size[10 * (i - 1)] = translated_size;
                    }
                        
                }
            }
            int wwwww = 0;
            timer = new System.Timers.Timer(100);
            //timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Enabled = true;
            timer.Elapsed += new System.Timers.ElapsedEventHandler(TimerUp);
            timer.Start();
            long currentTicks = DateTime.Now.Ticks;
            DateTime dtFrom = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            startMillis = (currentTicks - dtFrom.Ticks) / 10000;
        }
        //Timer的周期性操作函数
        private void TimerUp(object sender, System.Timers.ElapsedEventArgs e)
        {
            tickCount++;
            if (tickCount <= 500)
                updateBucket(tickCount);
            //这里还要设置一下包的大小，现在是都是1
            if (myTimeLine.PacketGoTimeLine[tickCount] == GREEN)
            {
                doBlockMove(GREEN, myTimeLine.PacketGoTimeLine_Size[tickCount]);
            }
            if (myTimeLine.PacketGoTimeLine[tickCount] == RED)
            {
                doBlockMove(RED, myTimeLine.PacketGoTimeLine_Size[tickCount]);
            }
            //long currentTicks = DateTime.Now.Ticks;
            //DateTime dtFrom = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            //long currentMillis = (currentTicks - dtFrom.Ticks) / 10000;
            //long timeElapsed = currentMillis - startMillis;

            //time.Text = tickCount.ToString();
        }
        private void doBlockMove(int out_color,int size)
        {
            //计数增加
            BlockMove(out_color, size, blueCount[size]);
            switch (out_color) {
                case RED: blueCount[size]++; redCount[size]++; break;
                case GREEN: blueCount[size]++; greenCount[size]++; break;
            }
        }
        //size取1,2,3。
        private void BlockMove(int out_color, int size, int count)
        {
            if (out_color == GREEN)
            {
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    int temp = (size - 1) * 8 + count - 1;
                    Animation.LeftSlide(blockList[temp], 0);
                    Animation.RightSlide(greenBlockList[temp], Animation.AppearDuration + Animation.MoveDurationLeft);
                }));
            }
            else if (out_color == RED)
            {
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    int temp = (size - 1) * 8 + count - 1;
                    Animation.LeftSlide(blockList[temp], 0);
                    Animation.RightSlide(redBlockList[temp], Animation.AppearDuration + Animation.MoveDurationLeft);
                }));
            }
            else
            {

            }
        }
        
        private void updateBucketPerStep(int before, int after)
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                Animation.Disappear(bucketList[myTimeLine.BucketShowTimeLine[before]]);
                Animation.Appear(bucketList[myTimeLine.BucketShowTimeLine[after]]);
            }));
        }

        //更新桶信息的主函数
        private void updateBucket(int tickCount)
        {

            if (myTimeLine.BucketShowTimeLine[tickCount] == myTimeLine.BucketShowTimeLine[tickCount - 1])
            {
                return;
            }
            else
            {
                updateBucketPerStep(tickCount - 1, tickCount);
            }
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
                    RightSideText.Text += tempShowId + "号分组到达时间：" + tempTime + "秒, 分组大小：" + tempSize + "比特。\n";
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
        private void Window_MouseLeftButtonDown_Drag(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
