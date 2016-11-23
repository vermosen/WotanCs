using IBApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Wotan
{
    public class client : eWrapperImpl
    {
        private logger log_;
        private EClientSocket socket_;

        public client(EReaderSignal reader, logger log)
        {
            log_ = log;
            socket_ = new EClientSocket(this, reader);
        }
        public EClientSocket socket
        {
            get { return socket_; }
            set { socket_ = value; }
        }

        public void addContract(Contract c, List<TagValue> options = null)
        {
            if (options == null) options = new List<TagValue>();
            socket_.reqMktData(1, c, "", false, options);
        }

        public void connect()
        {
            if (socket_.AsyncEConnect)
                socket_.startApi();
        }

        public void requestRealTimeBars(dataManager<realTimeBars> manager, 
                                        int correlationId, 
                                        Contract contract,
                                        int barSize,
                                        string whatToShow, 
                                        bool useRTH, 
                                        List<TagValue> realTimeBarsOptions = null)
        {
        }
    }
}
