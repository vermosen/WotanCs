using System;
using System.Diagnostics;
using Akka.Actor;
using System.Net;
using IBApi;
using System.Threading;

namespace Wotan
{
    // service implementation
    public class serviceImpl : service
    {
        private configurationContract config_;

        private IActorRef hist_;
        private IActorRef corr_;
        private IActorRef client_;

        // ctors
        [Obsolete]
        private serviceImpl() : base(null) { }                                  // for designer only
        public serviceImpl(string[] args) : base(args)
        {
            // correlation manager
            corr_ = actorSystem_.ActorOf(Props.Create<actors.correlationManager>(), "correlationActor");
        }

        // interfaces
        public override void onStartImpl(string[] args)
        {
            if (launchGatewayProcess())
            {
                // connection attempt
                try
                {
                    // create the client
                    client_ = actorSystem_.ActorOf(actors.client.Props(corr_, logger_), "clientActor");

                    client_.Tell(new connect(config_.ibEnvironment.credentials.host, config_.ibEnvironment.credentials.port));

                    // add historical data manager
                    hist_ = actorSystem_.ActorOf(actors.historicalDataManager.Props(client_, /*corr_,*/ logger_), "historicalManagerActor");

                    hist_.Tell(new actors.historicalDataManager.request(new Contract()
                    {
                        Symbol = "SDS",
                        SecType = "STK",
                        Exchange = "SMART",
                        Currency = "USD"
                    }, new DateTime(2016, 11, 27, 10, 28, 43),
                        new TimeSpan(10, 0, 0, 0, 0),
                        new TimeSpan(1, 0, 0, 0, 0),
                        actors.historicalDataManager.barType.MIDPOINT, 0, 1));

                    
                }
                catch (Exception ex)
                {
                    logger_?.Tell(new actors.log("an error has occurred: " + ex.ToString(),
                        logType.error, verbosity.high));
                }
            }
            else
            {
                logger_?.Tell(new actors.log("no running ibgateway process found, shutting down the service...",
                    logType.error, verbosity.high));

                Stop();
            }
        }
        public override void onStopImpl()
        {
            client_?.Tell(new disconnect());

            Thread.Sleep(5000);
        }
        public override void loadPreferencesImpl(string xmlPath)
        {
            config_ = (new contractSerializer<configurationContract>())
                .deserializeFromFile(xmlPath);

            // logger
            logger_ = actorSystem_.ActorOf(Props.Create<actors.logger>(
                config_.logger.create()), "logActor");
        }

        // methods
        private bool launchGatewayProcess()
        {
            Process[] pname = Process.GetProcessesByName("ibgateway");

            if (pname.Length != 0)
            {
                logger_?.Tell(new actors.log("ibgateway process is up and running",
                    logType.info, verbosity.high));

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
