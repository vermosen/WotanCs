using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBApi;

namespace Wotan
{
    public class eClient : eWrapperImpl
    {
        private logger log_;
        private EClientSocket socket_;
        private int nextOrder_;

        public eClient(logger log) : base()
        {
            log_ = log;
            socket_ = new EClientSocket(this, new EReaderMonitorSignal());
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
        public override void connectionClosed()
        {
            log_.log("closing connection with the distant service...", verbosity.high, messageType.error);
        }

        public override void error(Exception e)
        {
            log_.log(e.Message, verbosity.high, messageType.error);
        }

        public override void error(int id, int errorCode, string msg)
        {
            log_.log("error: " + msg, verbosity.high, messageType.error);
        }

        public override void currentTime(long time)
        {
            log_.log("time: " + time, verbosity.low, messageType.info);
        }

        public override void managedAccounts(string accountList)
        {
            log_.log("accounts: " + accountList, verbosity.medium, messageType.info);
        }

        public override void nextValidId(int orderId)
        {
            log_.log("id: " + orderId, verbosity.medium, messageType.info);
            nextOrder_ = orderId;
        }
    }
}
