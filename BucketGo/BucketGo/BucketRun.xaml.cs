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
        //Data实现动态绑定接口
        public class Data : INotifyPropertyChanged
        {
            private double time;
            private int size;

            public double Time {
                get
                {
                    return time;
                }
                set
                {
                    time = value;
                    Notify("Time");
                }
            }
            public int Size
            {
                get
                {
                    return size;
                }
                set
                {
                    size = value;
                    Notify("Size");
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;
            public void Notify(String info)
            {
                var handler = PropertyChanged;
                handler?.Invoke(this, new PropertyChangedEventArgs(info));
            }

        }

        //packet实现可排序接口
        public class Packet : IComparable <Packet>
        {
            int id;
            double arrivalTime;
            int size;
            public Packet()
            {
                arrivalTime = 0.0;
                size = 0;
            }
            public Packet(int id, double arrivalTime, int len)
            {
                this.id = id;
                this.arrivalTime = arrivalTime;
                this.size = len;
            }
            public int CompareTo(Packet p)
            {
                return this.id.CompareTo(p.id);
            }

        }
        public const int MAX_PACKET = 100;
        public const int Maximum = 10;
        public int currentPacketId = 0;
        public int[] a = new int[10];
        public Packet[] p = new Packet[MAX_PACKET];
        public Data d = new Data();
        public System.Timers.Timer timer;
        public long startMillis = 0;
        public int tickCount = 0;

        public MainWindow()
        {
            InitializeComponent();
            //imageList = { test };
            Image[] imageList = { littleblock };
            //image.IsEnabled = true;
            this.DataContext = d;
        }

        private void Button_Click_Start_Animation(object sender, RoutedEventArgs e)
        {
            //IsEnabled = false;
            timer = new System.Timers.Timer(100);
            //timer.Interval = TimeSpan.FromMilliseconds(100);
            //设置是否执行System.Timers.Timer.Elapsed事件
            timer.Enabled = true;
            //绑定Elapsed事件
            timer.Elapsed += new System.Timers.ElapsedEventHandler(TimerUp);
            //timer.Tick += timer_Tick;
            timer.Start();
            long currentTicks = DateTime.Now.Ticks;
            DateTime dtFrom = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            startMillis = (currentTicks - dtFrom.Ticks) / 10000;
            //int time = 0;
            Animation.LeftSlide(littleblock, 0);
            Animation.RightSlide(littleblock_red, Animation.AppearDuration + Animation.MoveDuration);
            //Animation.Appear(littlebucket16);
            Animation.Disappear(littlebucket16);
        }

        private void TimerUp(object sender, System.Timers.ElapsedEventArgs e)
        {
            tickCount++;
            if (tickCount % 10 == 0)
            {
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    Animation.Appear(littlebucket16);
                }));
            }
            if (tickCount % 10 == 5)
            {
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    Animation.Disappear(littlebucket16);
                }));
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

        //更新用户输入的新分组的信息
        private void updatePacket()
        {
            
            int tempId = currentPacketId;
            //double tempTime = Convert.ToDouble(TimeInput.ToString());
            //int tempSize = Convert.ToInt32(SizeInput.ToString());
            double tempTime = d.Time;
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

        private void Size_Input_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Time_Input_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_Throw_Packet(object sender, RoutedEventArgs e)
        {
            // Initialize a new Rectangle
            Rectangle r = new Rectangle();

            // Set up rectangle's size
            r.Width = 50;
            r.Height = 50;

            // Set up the Background color
            r.Fill = Brushes.Black;

            // Set up the position in the window, at mouse coordonate
            Canvas.SetTop(r, 50);
            Canvas.SetLeft(r, 50);

            // Add rectangle to the Canvas
            this.main.Children.Add(r);
        }
    }
}
