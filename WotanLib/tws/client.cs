using IBApi;
using System;

namespace Wotan
{
    public delegate void dispatchDelegate(twsMessage m);

    public class client : eWrapperImpl
    {
        private logDelegate log_;
        private dispatchDelegate dispatch_;
        private EClientSocket socket_;
        protected int serverVersion_;

        public client(EReaderMonitorSignal reader, dispatchDelegate dispatch, logDelegate log, bool aSync = true)
        {
            log_ = log;
            dispatch_ = dispatch;
            socket_ = new EClientSocket(this, reader) { AsyncEConnect = aSync };
        }
        public EClientSocket socket
        {
            get { return socket_; }
            private set { socket_ = value; }
        }

        public override void connectAck()
        {
            if (socket_.AsyncEConnect) socket_.startApi();
        }

        public override void historicalData(int reqId, string date, double open, double high, double low, double close, int volume, int count, double WAP, bool hasGaps)
        {
            dispatch_?.Invoke(new historicalData(reqId, date, open, high, low, close, volume, count, WAP, hasGaps));
        }        

        public override void historicalDataEnd(int reqId, string startDate, string endDate)
        {
            dispatch_?.Invoke(new historicalDataEnd(reqId, startDate, endDate));
        }
        public override void managedAccounts(string accountsList)
        {
            dispatch_?.Invoke(new managedAccounts(accountsList));
        }

        public override void nextValidId(int orderId)
        {
            //test
        }
        public override void error(Exception e)
        {
            error(e.ToString());
        }

        public override void error(string str)
        {
            log_?.Invoke(str, logType.error, verbosity.high);
        }

        public override void error(int id, int errorCode, string errorMsg)
        {
            error("[" + errorCode + "] " + errorMsg);
        }
        public override void updateNewsBulletin(int msgId, int msgType, string message, string origExchange)
        {
            log_?.Invoke(string.Format("[{0}] {1}", origExchange, message), logType.info, verbosity.high);
        }
    }
}