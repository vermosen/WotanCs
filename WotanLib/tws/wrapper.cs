using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBApi;
using System.Threading;

namespace Wotan
{
    public class wrapper : eWrapperImpl
    {
        private logger log_;
        private EClientSocket socket_;
        private readonly EReaderMonitorSignal signalReader_;
        private int nextOrder_;

        private realtimeBar barDlg_;

        public wrapper(logger log) : base()
        {
            log_ = log;
            signalReader_ = new EReaderMonitorSignal();
            socket_ = new EClientSocket(this, signalReader_);
            socket_.AsyncEConnect = false;
        }

        public void setRealTimeDelegate(realtimeBar barDlg)
        {
            barDlg_ = barDlg;
        }

        // interface
        public override void connectAck() {}
         
        public override void connectionClosed()
        {
            log_.log("closing connection with the distant service...", logType.error, verbosity.high);
        }

        public override void error(Exception e)
        {
            log_.log(e.Message, logType.error, verbosity.high);
        }

        public override void error(int id, int errorCode, string msg)
        {
            log_.log("error: " + msg, logType.error, verbosity.high);
        }

        public override void currentTime(long time)
        {
            log_.log("time: " + time, logType.info, verbosity.low);
        }

        public override void managedAccounts(string accountList)
        {
            log_.log("accounts: " + accountList, logType.info, verbosity.medium);
        }

        public override void nextValidId(int orderId)
        {
            log_.log("id: " + orderId, logType.info, verbosity.medium);
            nextOrder_ = orderId;
        }

        public override void realtimeBar(int reqId, long time, double open, double high, double low, double close, long volume, double WAP, int count)
        {
            log_.log("RealTimeBars. " + reqId +
                        " - Time: " + time +
                        ", Open: " + open +
                        ", High: " + high +
                        ", Low: " + low +
                        ", Close: " + close +
                        ", Volume: " + volume +
                        ", Count: " + count +
                        ", WAP: " + WAP, logType.info, verbosity.high);
        }

        public override void tickPrice(int tickerId, int field, double price, int canAutoExecute)
        {
            log_.log("Tick Price. Ticker Id:" + tickerId + ", Field: " + field +
                              ", Price: " + price + ", CanAutoExecute: " + canAutoExecute, logType.info, verbosity.high);
        }

        public override void tickSize(int tickerId, int field, int size)
        {
            Console.WriteLine("Tick Size. Ticker Id:" + tickerId +
                              ", Field: " + field + ", Size: " + size, logType.info, verbosity.high);
        }
    }
}
