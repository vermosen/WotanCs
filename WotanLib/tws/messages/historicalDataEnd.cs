using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wotan
{
    public class historicalDataEnd : message
    {
        private const string format = "yyyyMMdd  hh:mm:ss";

        public int reqId { get; private set; }
        public DateTime startDate { get; private set; }
        public DateTime endDate { get; private set; }

        public historicalDataEnd(int reqId, string startDate, string endDate) : base(messageType.historicalDataEnd)
        {
            this.reqId = reqId;
            this.startDate = DateTime.ParseExact(startDate, format, CultureInfo.InvariantCulture);
            this.endDate = DateTime.ParseExact(endDate, format, CultureInfo.InvariantCulture);
        }
    }
}
