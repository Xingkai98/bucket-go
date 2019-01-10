using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BucketGo
{
    public class Timeline
    {
        public const int RED = 101;
        public const int GREEN = 102;
        //目前时间线大小为1000即，100秒
        //根据时间线定义每个时刻的Bucket大小
        //public int[] BucketTimeLine = new int[1000];
        //public int[] BucketShowTimeLine = new int[1000];
        //public int[] PacketGoTimeLine = new int[1000];
        public List<int> BucketTimeLine = new List<int>(1000);
        public List<int> BucketShowTimeLine = new List<int>(1000);
        public List<bool> IfPacketTimeLine = new List<bool> { false, false , true, false, false };
        public List<int> PacketGoTimeLine = new List<int>(1000);
        public Timeline()
        {
            //初始化BucketTimeLine
            for(int i = 0; i < 1000; i++)
            {
                BucketTimeLine.Add(i);
            }
            getShowTimeLine();

        }
        public void getShowTimeLine()
        {
            int max = BucketTimeLine.Max();
            int min = BucketTimeLine.Min();
            int div = (max - min) / 16;
            for (int i = 0; i < 1000; i++)
            {
                double temp = (double)BucketTimeLine[i] / (double)max * (double)16;
                BucketShowTimeLine.Add((int)temp);
            }
        }
    }
}
