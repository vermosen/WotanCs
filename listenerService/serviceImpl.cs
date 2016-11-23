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
        private EReaderSignal reader_;
        private client cli_;

        // ctors
        [Obsolete]
        private serviceImpl() : base(null) { }                                  // for designer only
        public serviceImpl(string[] args) : base(args) {}

        // interfaces
        public override void onStartImpl(string[] args)
        {
            if (launchGatewayProcess())
            {
                reader_ = new EReaderMonitorSignal();

                // connection attempt
                try
                {
                    cli_ = new client(reader_, log_);
                    cli_.socket.eConnect("127.0.0.1", config_.interactiveBroker.port, 0);
                    
                    cli_.addContract(new Contract()
                    {
                        Symbol = "SDS",
                        SecType = "STK",
                        Exchange = "SMART",
                        Currency = "USD"
                    });
                }
                catch (Exception ex)
                {
                    log_.log("an error has occurred: " + ex.ToString(),
                        logType.error, verbosity.high);
                }
            }
            else
            {
                log_.log("no running ibgateway process found, shutting down the service...",
                    logType.info, verbosity.high);

                Stop();
            }
        }
        public override void onStopImpl()
        {
            if (cli_ != null)
            {
                cli_.socket.eDisconnect();
            }

        }
        public override void loadPreferencesImpl(string xmlPath)
        {
            config_ = (new xmlParser<configuration>()).ToObject(new FileStream(xmlPath, FileMode.Open));

            // set the logger
            if (config_.logger != null) log_ = config_.logger.create();
        }

        // methods
        private bool launchGatewayProcess()
        {
            Process[] pname = Process.GetProcessesByName("ibgateway");

            if (pname.Length != 0)
            {
                log_.log("ibgateway process is up and running",
                    logType.info, verbosity.medium);

                return true;
            }
            else
            {
                // TODO: autologin
                //int nAttempt = 0; bool lanched = false;

                //ProcessStartInfo psi = new ProcessStartInfo();
                //psi.FileName = config_.environment.javaPath + @"/java";
                //psi.RedirectStandardInput = true;
                //psi.RedirectStandardOutput = false;
                ////psi.Arguments =...
                //psi.UseShellExecute = false;

                //do
                //{
                //    if (lanched)
                //        return true;
                //    else
                //        Thread.Sleep(config_.interactiveBroker.delayAttempt);
                //}
                //while (++nAttempt < config_.interactiveBroker.maxAttempt);

                return false;
            }
        }
    }
}
