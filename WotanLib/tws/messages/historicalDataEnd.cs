using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wotan
{
    public class historicalDataEnd : message
    {
        public int reqId { get; private set; }
        public DateTime startDate { get; private set; }
        public DateTime endDate { get; private set; }

        public historicalDataEnd(int reqId, string startDate, string endDate) : base(messageType.historicalDataEnd)
        {
            this.reqId = reqId;
            this.startDate = Convert.ToDateTime(startDate);
            this.endDate = Convert.ToDateTime(endDate);
        }
    }
}
