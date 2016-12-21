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
        public historicalRequest(Contract c, DateTime t, TimeSpan t1, TimeSpan t2, barType b, int i1, int i2)
        {

        }
    }

    public class historicalManager : manager, IHandle<historicalData>, IHandle<historicalRequest>
    {
        public static Props Props(IActorRef client, IActorRef logger)
        {
            return Akka.Actor.Props.Create(() => new historicalManager(client, logger));
        }

        public historicalManager(IActorRef client, IActorRef logger) : base(client, logger) {}

        public void Handle(historicalData message)
        {
            throw new NotImplementedException();
        }

        public void Handle(historicalRequest message)
        {
            client_?.Tell(message);
        }
    }
}
