using IBApi;
using System;

namespace Wotan
{
    // historical data management /
    public delegate void updateDelegate(message message);

    public class messageDispatcher
    {
        private logger log_;

        private updateDelegate[] map_;
        public messageDispatcher(logger log)
        {
            log_ = log;
            map_ = new updateDelegate[101];

            for (int i = 0; i< map_.Length; i++)
            {
                map_[i] += blackHole;
            }
        }

        public void register(Tuple<messageType, updateDelegate>[] dlgs)
        {
            foreach (var i in dlgs)
            {
                map_[(int)i.Item1] += i.Item2;
            }
        }

        public void dispatch(message m)
        {
            map_[(int)m.type]?.Invoke(m);
        }

        private void blackHole(message m) {}
    }

    public class client : eWrapperImpl
    {
        private actors.logDlg log_;
        private actors.dispatchDlg dispatch_;
        private EClientSocket socket_;

        protected bool isConnected_;
        protected int serverVersion_;

        public client(EReaderMonitorSignal reader, actors.dispatchDlg dispatch, actors.logDlg log, bool aSync = true)
        {
            isConnected_ = false;
            log_ = log;
            dispatch_ = dispatch;
            socket_ = new EClientSocket(this, reader) { AsyncEConnect = aSync };
        }
        public EClientSocket socket
        {
            get { return socket_; }
            set { socket_ = value; }
        }

        public override void connectAck()
        {
            if (socket_.AsyncEConnect)
                socket_.startApi();
        }

        protected bool checkConnection()
        {
            if (!isConnected_)
            {
                error(IncomingMessage.NotValid, EClientErrors.NOT_CONNECTED.Code, EClientErrors.NOT_CONNECTED.Message);
                return false;
            }

            return true;
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
            log_?.Invoke(new actors.log(str, logType.error, verbosity.high));
        }

        public override void error(int id, int errorCode, string errorMsg)
        {
            error("[" + errorCode + "] " + errorMsg);
        }
    }
}