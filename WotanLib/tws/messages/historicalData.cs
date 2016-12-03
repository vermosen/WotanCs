using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wotan
{
    public class historicalData : message
    {
        private const string format = "yyyyMMdd";

        public historicalData(  int reqId, string date, double open, double high, double low, 
                                double close, int volume, int count, double WAP, bool hasGaps) : base(messageType.historicalData)
        {
            this.reqId = reqId;
            this.date = DateTime.ParseExact(date, format, CultureInfo.InvariantCulture);
            this.open = open;
            this.high = high;
            this.low = low;
            this.close = close;
            this.volume = volume;
            this.count = count;
            this.WAP = WAP;
            this.hasGaps = hasGaps;
        }

        public int reqId { get; private set; }
        public DateTime date { get; private set; }
        public double open { get; private set; }
        public double high { get; private set; }
        public double low { get; private set; }
        public double close { get; private set; }
        public int volume { get; private set; }
        public int count { get; private set; }
        public double WAP { get; private set; }
        public bool hasGaps { get; private set; }
    }
}
