using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wotan.actors
{
    // client is strongly typed
    class client : TypedActor, IHandle<message>
    {
        private readonly IActorRef dispatcher_;
        private readonly IActorRef correlationManager_;

        public void Handle(message message)
        {
            throw new NotImplementedException();
        }
    }
}
