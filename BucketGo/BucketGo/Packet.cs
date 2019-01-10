using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BucketGo
{
    //packet实现可排序接口
    public class Packet : IComparable<Packet>
    {
        int id;
        int arrivalTime;
        int size;
        public Packet()
        {
            arrivalTime = 0;
            size = 0;
        }
        public Packet(int id, int arrivalTime, int len)
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
}
