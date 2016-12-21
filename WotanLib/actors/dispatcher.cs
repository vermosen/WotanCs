using Akka.Actor;
using System;
using System.Collections.Generic;

namespace Wotan.actors
{
    // registration signal tells a dispatcher which 
    public class registration : signal
    {
        public IActorRef actor { get; private set; }
        public messageType[] types { get; private set; }
    }

    public class dispatcher : TypedActor, IHandle<message>, IHandle<registration>
    {
        private IActorRef log_;

        // TODO : see if we may have a static array instead...
        // may have to change to IActorRef or List of IActorRef...
        private Dictionary<messageType, LinkedList<IActorRef>> map_;

        public static Props Props(IActorRef logger)
        {
            return Akka.Actor.Props.Create(() => new dispatcher(logger));
        }


        public void Handle(message m)
        {
            foreach (var i in map_[m.type])
            {
                i.Tell(m);
            }
        }

        protected override void Unhandled(object message)
        {
            //Do something with the message.
            log_.Tell(new log("bla", logType.warning, verbosity.high));
        }

        public dispatcher(IActorRef log)
        {
            log_ = log;
            map_ = new Dictionary<messageType, LinkedList<IActorRef>>();
        }

        public void Handle(registration message)
        {
            foreach (var i in message.types)
            {
                map_[i].AddLast(message.actor);
            }
        }
    }
}
