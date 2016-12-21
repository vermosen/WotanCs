using Akka.Actor;
using IBApi;
using System.Net;
using System.Threading;
using System;

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
    public class disconnect : signal { }

    public delegate void dispatchDlg(message m);

    // client is strongly typed
    public class client : TypedActor, IHandle<connect>, IHandle<disconnect>, IHandle<historicalRequest>
    {
        private readonly IActorRef dispatcher_;
        private readonly IActorRef correlationManager_;
        private readonly IActorRef logger_;

        private readonly Wotan.client   client_;
        private EReaderSignal           signal_;

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

        public void Handle(connect message)
        {
            if (!client_.socket.IsConnected())
            {
                // connect
                client_.socket.eConnect(message.host.ToString(), message.port, 0);
                var reader = new EReader(client_.socket, signal_);
                reader.Start();

                new Thread(() => { while (client_.socket.IsConnected()) { signal_.waitForSignal(); reader.processMsgs(); } })
                {
                    Name = "reading thread",
                    IsBackground = true
                }.Start();
            }
        }

        public void Handle(disconnect message)
        {
            if (client_.socket.IsConnected())
            {
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

        // callbacks
        private void dispatch(message m)
        {
            dispatcher_.Tell(m);
        }

        private void log(log m)
        {
            logger_.Tell(m);
        }
    }
}
