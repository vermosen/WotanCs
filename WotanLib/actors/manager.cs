using Akka.Actor;

namespace Wotan.actors
{
    public abstract class manager : TypedActor
    {
        protected IActorRef client_;
        protected IActorRef logger_;

        public manager(IActorRef client, IActorRef logger)
        {
            client_ = client;
            logger_ = logger;
        }
    }
}
