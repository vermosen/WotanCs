using Akka.Actor;
using System;

namespace Wotan.actors
{
    public class historicalManager : manager, IHandle<historicalData>
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
    }
}
