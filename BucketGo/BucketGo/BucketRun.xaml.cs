using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
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

//
namespace BucketGo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        //packet实现可排序接口
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
            Animation.LeftSlide(littleblock, 0);
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
            currentPacketId++;
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                //存储时，分组的Id是从0开始的，而展示时分组的Id是从1开始的
                int tempShowId = currentPacketId + 1;
                RightSideText.Text += tempShowId + "号分组到达时间：" + tempTime + "ms, 分组大小：" + tempSize + "字节。\n";
            }));
        }

        private void Size_Input_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Time_Input_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
