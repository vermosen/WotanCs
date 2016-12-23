using Akka.Actor;

namespace Wotan.actors
{
    public abstract class manager : TypedActor
    {
        protected IActorRef client_;
        protected IActorRef logger_;
        //protected IActorRef corr_;

        public manager(IActorRef client, /*IActorRef corr, */IActorRef logger)
        {
            client_ = client;
            logger_ = logger;
            //corr_   = corr  ;
        }
    }
}
