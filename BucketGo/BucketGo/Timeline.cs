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
        //根据时间线定义每个时刻的Bucket大小
        public List<int> BucketTimeLine = new List<int>(1000) { 1,1,1,10,10,10,20,20,20,40,40,40,50,50,50,60,60,60,70,70,70};
        public List<int> BucketShowTimeLine = new List<int>(1000);
        //public List<bool> IfPacketTimeLine = new List<bool> { false, false , true, false, false };
        public List<int> PacketGoTimeLine = new List<int>(1000);
        public Timeline()
        {
            //初始化BucketTimeLine
            getShowTimeLine();

        }
        public void getShowTimeLine()
        {
            int max = BucketTimeLine.Max();
            int min = BucketTimeLine.Min();
            int div = (max - min) / 16;
            foreach(int i in BucketTimeLine)
            {
                double temp = i / max * 16;
                BucketShowTimeLine.Add((int)temp);
            }
        }
    }
}
