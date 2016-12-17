using Akka.Actor;
using System;
using System.Collections.Generic;

namespace Wotan.actors
{
    public class dispatcher : TypedActor, IHandle<message>
    {
        private IActorRef log_;

        // TODO : see if we may have a static array instead...
        // may have to change to IActorRef or List of IActorRef...
        private Dictionary<messageType, updateDelegate> map_;

        public static Props Props(IActorRef logger)
        {
            return Akka.Actor.Props.Create(() => new dispatcher(logger));
        }


        public void Handle(message m)
        {
            map_[m.type]?.Invoke(m);
        }

        protected override void Unhandled(object message)
        {
            //Do something with the message.
        }

        public dispatcher(IActorRef log)
        {
            log_ = log;
            map_ = new Dictionary<messageType, updateDelegate>();
        }

        public void register(Tuple<messageType, updateDelegate>[] dlgs)
        {
            foreach (var i in dlgs)
            {
                map_[i.Item1] += i.Item2;
            }
        }
    }
}
