using System;
using Akka.Actor;

namespace Wotan.actors
{
    public class correlationManager : TypedActor, IHandle<IMessage>
    {
        public class request : IMessage {}
        public class reply : IMessage
        {
            public correlation<int> correlation { get; private set; }
            public reply(correlation<int> id) { correlation = id; }
        }

        // members
        private int previous_;

        public correlationManager() { previous_ = 0; }

        public correlation<int> next()
        {
            return new correlation<int>(previous_ + 1);
        }

        public void Handle(IMessage m)
        {
            if (m.GetType() == typeof(request))
            {
                Sender.Tell(new reply(next()), Self);
            }
        }
    }
}
