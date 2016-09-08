using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBApi;
using System.Diagnostics;
using System.Threading;

namespace Wotan
{
    // service implementation
    public class serviceImpl : service
    {
        private configuration config_;
        private eClient cli_;
        
        // ctors
        [Obsolete]
        private serviceImpl() : base(null) { }                                  // for designer only
        public serviceImpl(logger log) : base(log) { }

        // interfaces
        public override void onStartImpl()
        {
            cli_ = new eClient(log_);
            cli_.socket.eConnect("127.0.0.1", 7496, 0);
        }
        public override void onStopImpl()
        {
            cli_.socket.eDisconnect();
        }
        public override void loadPreferencesImpl(string xmlPath)
        {
            config_ = (new xmlParser<configuration>()).ToObject(new FileStream(xmlPath, FileMode.Open));
        }

        // methods
        private bool launchGatewayProcess()
        {
            Process[] pname = Process.GetProcessesByName("ibgateway");

            if (pname.Length != 0)
            {
                log_.log("ibgateway process is up and running",
                    verbosity.medium, messageType.info);

                return true;
            }
            else
            {
                int nAttempt = 0; bool lanched = false;

                do
                {
                    if (lanched)
                        return true;
                }
                while (++nAttempt < config_.interactiveBroker.maxAttempt);

                return false;
            }
        }
    }
}
