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
        private EReaderMonitorSignal signal_;
        private client cli_;
        private historicalDataManager hist_;

        // ctors
        [Obsolete]
        private serviceImpl() : base(null) { }                                  // for designer only
        public serviceImpl(string[] args) : base(args)
        {
            signal_ = new EReaderMonitorSignal();
        }

        // interfaces
        public override void onStartImpl(string[] args)
        {
            if (launchGatewayProcess())
            {
                // connection attempt
                try
                {
                    cli_ = new client(signal_, log_);
                    cli_.socket.eConnect("127.0.0.1", config_.interactiveBroker.port, 0);

                    var reader = new EReader(cli_.socket, signal_);
                    reader.Start();

                    new Thread(() => { while (cli_.socket.IsConnected()) { signal_.waitForSignal(); reader.processMsgs(); } })
                    {
                        Name = "reading thread",
                        IsBackground = true
                    }.Start();

                    // add historical data manager
                    hist_ = new historicalDataManager(cli_);

                    hist_.addRequest(new Contract()
                    {
                        Symbol = "SDS",
                        SecType = "STK",
                        Exchange = "SMART",
                        Currency = "USD"
                    }, "20161127 10:28:43", "10 D", "1 day", "MIDPOINT", 0, 1);

                }
                catch (Exception ex)
                {
                    log_.add("an error has occurred: " + ex.ToString(),
                        logType.error, verbosity.high);
                }
            }
            else
            {
                log_.add("no running ibgateway process found, shutting down the service...",
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
            config_ = (new xmlParser<configuration>()).ToObject(
                new FileStream(Path.GetFullPath(xmlPath), FileMode.Open, FileAccess.Read, FileShare.Read));

            // set the logger
            if (config_.logger != null) log_ = config_.logger.create();
        }

        // methods
        private bool launchGatewayProcess()
        {
            Process[] pname = Process.GetProcessesByName("ibgateway");

            if (pname.Length != 0)
            {
                log_.add("ibgateway process is up and running",
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
