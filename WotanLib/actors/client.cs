using IBApi;
using System.Net;
using System.Threading;
using System;

namespace Wotan.actors
{
    public delegate void dispatchDlg(twsMessage m);

    // client is strongly typed
    public class client : TypedActor, IHandle<IMessage>
    {
        private readonly IActorRef dispatcher_;
        private readonly IActorRef correlationManager_;
        private readonly IActorRef logger_;

        private readonly Wotan.client   client_;
        private EReaderMonitorSignal    signal_;
        private EReader                 reader_;

        public static Props Props(IActorRef correlationManager, IActorRef logger)
        {
            return Akka.Actor.Props.Create(() => new client(correlationManager, logger));
        }

        public client(IActorRef correlationManager, IActorRef logger)
        {
            // actors
            logger_ = logger;
            correlationManager_ = correlationManager;
            dispatcher_ = Context.ActorOf(dispatcher.Props(logger_), "clientActor");

            // tws components
            signal_ = new EReaderMonitorSignal();
            client_ = new Wotan.client(signal_, new dispatchDlg(dispatch), new logDlg(log), aSync:false);
        }

        public void Handle(IMessage m)
        {
            if (m.GetType() == typeof(connect))
            {
                (new Thread(() => connect((m as connect).host, (m as connect).port))).Start();
            }
            else if (m.GetType() == typeof(disconnect))
            {
                if (client_.socket.IsConnected())
                {
                    logger_?.Tell(new log("aborting connection", logType.info, verbosity.high));

                    // connect
                    client_.socket.eDisconnect();
                }
            }
            else if (m.GetType() == typeof(connectionStatus))
            {
                if (client_.socket.IsConnected())
                {
                    Sender.Tell(new connectionStatus(true), Self);
                }
            }
            else if (m.GetType() == typeof(historicalDataManager.request))
            {
                if (client_.socket.IsConnected())
                {
                    var temp = (m as historicalDataManager.request);

                    if (temp.correlation != null)
                    {
                        client_.socket.reqHistoricalData(
                            temp.correlation.id,
                            temp.contract,
                            temp.endDatetime.ToString("yyyyMMdd"),
                            temp.duration.ToString(),
                            temp.barSize.ToString(),
                            temp.type.ToString(),
                            temp.useRTH,
                            temp.formatDate,
                            null);
                    }
                    
                }
            }
            else if (m.GetType() == typeof(registration))
            {
                // pass-through
                dispatcher_?.Forward(m);
            }
        }

        protected override void Unhandled(object message)
        {
            // test
            logger_.Tell(new log("unhandled message found in client", logType.warning, verbosity.medium));
        }

        // connect
        private void connect(IPAddress host, int port)
        {
            if (!client_.socket.IsConnected())
            {
                client_.socket.eConnect(host.ToString(), port, 1);
                reader_ = new EReader(client_.socket, signal_);
                reader_.Start();

                new Thread(() => 
                {
                    while (client_.socket.IsConnected())
                    {
                        signal_.waitForSignal();
                        reader_.processMsgs();
                    }
                }) { Name = "reading thread", IsBackground = true }.Start();
            }
        }

        // callbacks
        private void dispatch(twsMessage m)
        {
            dispatcher_?.Tell(m);
        }

        private void log(log m)
        {
            logger_?.Tell(m);
        }
    }
}
