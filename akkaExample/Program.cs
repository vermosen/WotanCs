using System;
using System.Collections.Generic;
using Wotan;

namespace akkaExample
{
    // ------------- scheme ---------------
    //             worker 
    //           /         \
    //     sink <- worker <- correlation
    //           \         /
    //             worker 

    public class worker : UntypedActor
    {
        public worker(correlationManager mgr)
        {

        }

        public void query(int nCorr)
        {

        }

        protected override void OnReceive(object message)
        {
            throw new NotImplementedException();
        }
    }

    public class sink : UntypedActor
    {
        private readonly worker[] workers_;
        private List<correlation<int> > ids_ = new List<correlation<int> >();

        public sink(worker[] workers)
        {
            workers_ = workers;
        }

        public void doWork(int nCorr)
        {
            foreach (var w in workers_)
            {
                w.query(nCorr);
            }
        }

        protected override void OnReceive(object message)
        {
            ids_.Add((correlation<int>)message);
        }
    }

    class Program
    {
        public static ActorSystem actorSystem_;
        public static IActorRef sink_;

        static void Main(string[] args)
        {

            actorSystem_ = ActorSystem.Create("actorSystem");

            IActorRef correlation = actorSystem_.ActorOf(Props.Create<correlationManager>(), "correlationManager");

            IActorRef[] workers_ = new IActorRef[10];

            var p = Props.Create<worker>(correlation);

            for (int i = 0; i < workers_.Length; i++)
            {
                workers_[i] = actorSystem_.ActorOf(p);
            }

            IActorRef sink_ = actorSystem_.ActorOf(Props.Create<sink>(workers_), "sink");
        }
    }
}
