using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BucketGo
{
    //Data实现动态绑定接口
    public class Data : INotifyPropertyChanged
    {
        private int time;
        private int size;
        private int bucket;
        private int speed;

        public int Time
        {
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
        public int Bucket
        {
            get
            {
                return bucket;
            }
            set
            {
                bucket = value;
                Notify("Bucket");
            }
        }
        public int Speed
        {
            get
            {
                return speed;
            }
            set
            {
                speed = value;
                Notify("Speed");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void Notify(String info)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(info));
        }

    }
}
