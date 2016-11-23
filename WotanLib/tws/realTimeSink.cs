using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wotan
{
    public struct bar
    {
        long time;
        double open;
        double high;
        double low;
        double close;
        long volume;
        double WAP;
        int count;
    }

    public delegate void realtimeBar(int reqId, bar data);

    public class realTimeSink : sink
    {
        public realTimeSink(wrapper w) : base(w)
        {
            w.setRealTimeDelegate(new realtimeBar(realtimeBar));
        }

        public void realtimeBar(int reqId, bar data)
        {

        }
    }
}
