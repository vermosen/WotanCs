using Akka.Actor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wotan.actors
{
    public class dispatcher : TypedActor, IHandle<message>
    {
        private logger log_;

        // TODO : see if we may have a static array instead...
        // may have to change to IActorRef or List of IActorRef...
        private Dictionary<messageType, updateDelegate> map_;

        public void Handle(message m)
        {
            map_[m.type]?.Invoke(m);
        }

        protected override void Unhandled(object message)
        {
            //Do something with the message.
        }

        public dispatcher(logger log)
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
