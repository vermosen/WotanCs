using Akka.Actor;

namespace Wotan.actors
{
    public abstract class dataManager : TypedActor
    {
        protected IActorRef client_;
        protected IActorRef logger_;
        protected IActorRef corr_;

        public dataManager(IActorRef client, IActorRef corr, IActorRef logger)
        {
            client_ = client;
            logger_ = logger;
            corr_   = corr  ;
        }
    }
}
