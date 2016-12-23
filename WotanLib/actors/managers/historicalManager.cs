using Akka.Actor;
using IBApi;
using System;

namespace Wotan.actors
{
    public enum barType
    {
        MIDPOINT = 1,
        unknown = 0
    }

    public class historicalRequest : signal
    {
        public Contract contract { get; private set; }
        public DateTime datetime { get; private set; }
        public TimeSpan t1 { get; private set; }
        public TimeSpan t2 { get; private set; }
        public barType b { get; private set; }
        public int i1 { get; private set; }
        public int i2 { get; private set; }

        public historicalRequest(Contract contract, DateTime datetime, TimeSpan t1, TimeSpan t2, barType b, int i1, int i2)
        {
            this.contract = contract;
            this.datetime = datetime;
            this.t1 = t1;
            this.t2 = t2;
            this.b = b;
            this.i1 = i1;
            this.i2 = i2;
        }
    }

    public class historicalManager : manager, IHandle<historicalData>, IHandle<historicalRequest>
    {
        public static Props Props(IActorRef client, /*IActorRef corr,*/ IActorRef logger)
        {
            return Akka.Actor.Props.Create(() => new historicalManager(client, /*corr,*/ logger));
        }

        public historicalManager(IActorRef client, /*IActorRef corr,*/ IActorRef logger) : base(client, /*corr,*/ logger)
        {
            // register
            client_.Tell(new registration(Self, new messageType[] { messageType.historicalData }));
        }

        public void Handle(historicalData message)
        {
            throw new NotImplementedException();
        }

        public void Handle(historicalRequest message)
        {
            // pass-through
            client_.Tell(message);
        }
    }
}
