using Akka.Actor;
using IBApi;
using System.Net;
using System.Threading;

namespace Wotan.actors
{
    public class connect : signal
    {
        public IPAddress host { get; private set; }
        public int port { get; private set; }

        public connect(IPAddress host, int port)
        {
            this.host = host;
            this.port = port;
        }
    }
    public class disconnect : signal {}

    public delegate void dispatchDlg(message m);

    // client is strongly typed
    public class client : TypedActor, IHandle<connect>, IHandle<disconnect>, IHandle<historicalRequest>, IHandle<registration>
    {
        private readonly IActorRef dispatcher_;
        private readonly IActorRef correlationManager_;
        private readonly IActorRef logger_;

        private readonly Wotan.client   client_;
        private EReaderSignal           signal_;
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
            client_ = new Wotan.client(signal_, new dispatchDlg(dispatch), new logDlg(log));
        }

        public void Handle(connect m)
        {
            (new Thread(() => connect(m.host, m.port))).Start();
        }

        public void Handle(disconnect m)
        {
            if (client_.socket.IsConnected())
            {
                logger_?.Tell(new log("aborting connection", logType.info, verbosity.high));

                // connect
                client_.socket.eDisconnect();
            }
        }

        public void Handle(historicalRequest message)
        {
            if (client_.socket.IsConnected())
            {

            }
        }

        public void Handle(registration m)
        {
            // pass-through
            dispatcher_?.Tell(m);
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
        private void dispatch(message m)
        {
            dispatcher_?.Tell(m);
        }

        private void log(log m)
        {
            logger_?.Tell(m);
        }
    }
}
