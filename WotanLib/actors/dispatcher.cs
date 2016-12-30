using Akka.Actor;
using System;
using System.Collections.Generic;

namespace Wotan.actors
{

    // registration signal tells a dispatcher which 
    public class registration : IMessage
    {
        public IActorRef actor { get; private set; }
        public messageType[] types { get; private set; }

        public registration(IActorRef actor, messageType[] types)
        {
            this.actor = actor;
            this.types = types;
        }
    }

    public class dispatcher : TypedActor, IHandle<twsMessage>, IHandle<registration>
    {
        private IActorRef logger_;

        // TODO : see if we may have a static array instead...
        // may have to change to IActorRef or List of IActorRef...
        private Dictionary<messageType, LinkedList<IActorRef>> map_;

        public static Props Props(IActorRef logger)
        {
            return Akka.Actor.Props.Create(() => new dispatcher(logger));
        }

        public void Handle(twsMessage m)
        {
            if (map_.ContainsKey(m.type))
            {
                foreach (var i in map_[m.type])
                {
                    i.Tell(m);
                }
            }
        }

        protected override void Unhandled(object message)
        {
            //Do something with the message.
            logger_.Tell(new log("bla", logType.warning, verbosity.high));
        }

        public dispatcher(IActorRef log)
        {
            logger_ = log;
            map_ = new Dictionary<messageType, LinkedList<IActorRef>>();
        }

        public void Handle(registration m)
        {
            foreach (var i in m.types)
            {
                if (!map_.ContainsKey(i))
                {
                    map_.Add(i, new LinkedList<IActorRef>());
                }

                map_[i].AddLast(m.actor);
            }
        }
    }
}
