using System;
using System.Diagnostics;
using System.Net;
using IBApi;
using System.Threading;
using System.Threading.Tasks;

namespace Wotan
{
    // service implementation
    public class serviceImpl : service
    {
        private correlationManager corr_;
        private client client_;

        private EReaderMonitorSignal signal_;

        // ctors
        [Obsolete]
        private serviceImpl() : base(null) { }                                  // for designer only
        public serviceImpl(string[] args) : base(args)
        {
            client_ = null;
            logger_ = null;
        }

        // interfaces
        public override void onStartImpl(string[] args)
        {
            if (launchGatewayProcess())
            {
                // connection attempt
                try
                {
                    var conf = (configuration)config_;

                    logger_ = conf.logger.create();
                    signal_ = new EReaderMonitorSignal();
                    corr_ = new correlationManager();

                    //dispatcher_ = new dis
                    // tws components
                    //client_ = new client(signal_, new dispatchDelegate(dispatch), new logDelegate(logger_.add), aSync: false);

                    // create the client
                    //client_ = actorSystem_.ActorOf(actors.client.Props(corr_, logger_), "clientActor");

                    //client_.Tell(new connect(config_.ibEnvironment.credentials.host, config_.ibEnvironment.credentials.port));

                    //// wait for the connection
                    //while (Task.Run(async () =>
                    //{
                    //    var t = client_.Ask<connectionStatus>(new connectionStatus(), TimeSpan.FromSeconds(10));
                    //    await t;
                    //    return t.Result.isConnected;
                    //}).Result != true)
                    //{
                    //    Thread.Sleep(100);
                    //}

                    //// add historical data manager
                    //hist_ = actorSystem_.ActorOf(actors.historicalDataManager.Props(client_, corr_, logger_), "historicalManagerActor");

                    //hist_.Tell(new actors.historicalDataManager.request(new Contract()
                    //{
                    //    Symbol = "SDS",
                    //    SecType = "STK",
                    //    Exchange = "SMART",
                    //    Currency = "USD"
                    //}, new DateTime(2016, 11, 27, 10, 28, 43),
                    //    TimeSpan.FromDays(10),
                    //    TimeSpan.FromDays(1),
                    //    actors.historicalDataManager.barType.MIDPOINT, 0, 1));
                }
                catch (Exception ex)
                {
                    logger_?.add("an error has occurred: " + ex.ToString(),
                        logType.error, verbosity.high);
                }
            }
            else
            {
                logger_?.add("no running ibgateway process found, shutting down the service...",
                    logType.error, verbosity.high);

                Stop();
            }
        }
        public override void onStopImpl()
        {
            client_?.socket.eDisconnect();
            Thread.Sleep(5000);
        }
        public override void loadPreferencesImpl(string xmlPath)
        {
            config_ = (new contractSerializer<configuration>())
                .deserializeFromFile(xmlPath);

            // logger
            logger_ = (config_ as configuration).logger.create();
        }

        // methods
        private bool launchGatewayProcess()
        {
            Process[] pname = Process.GetProcessesByName("ibgateway");

            if (pname.Length != 0)
            {
                logger_?.add("ibgateway process is up and running",
                    logType.info, verbosity.high);

                return true;
            }
            else
            {
                // TODO: launch the gateway process
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
