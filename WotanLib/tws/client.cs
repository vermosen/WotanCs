using IBApi;
using System;

namespace Wotan
{
    // historical data management
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
        private logger log_;
        private EClientSocket socket_;

        protected bool isConnected_;
        protected int serverVersion_;

        public messageDispatcher dispatcher { get; set; }

        public client(EReaderSignal reader, logger log)
        {
            isConnected_ = false;
            log_ = log;
            socket_ = new EClientSocket(this, reader);
            dispatcher = new messageDispatcher(log_);
        }
        public EClientSocket socket
        {
            get { return socket_; }
            set { socket_ = value; }
        }

        public void connect()
        {
            if (socket_.AsyncEConnect)
                socket_.startApi();
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
            dispatcher?.dispatch(new historicalData(reqId, date, open, high, low, close, volume, count, WAP, hasGaps));
        }        

        public override void historicalDataEnd(int reqId, string startDate, string endDate)
        {
            dispatcher?.dispatch(new historicalDataEnd(reqId, startDate, endDate));
        }
        public override void managedAccounts(string accountsList)
        {
            dispatcher?.dispatch(new managedAccounts(accountsList));
        }

        public override void error(Exception e)
        {
            error(e.ToString());
        }

        public override void error(string str)
        {
            log_?.add(str, logType.error, verbosity.high);
        }

        public override void error(int id, int errorCode, string errorMsg)
        {
            error("[" + errorCode + "] " + errorMsg);
        }
    }
}