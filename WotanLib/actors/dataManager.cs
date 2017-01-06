using Akka.Actor;
using System;
using System.Threading.Tasks;

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

        public async Task<correlation<int>> getCorrelation()
        {
            var t1 = corr_.Ask<correlationManager.reply>(new correlationManager.request(), TimeSpan.FromSeconds(1));
            await t1;
            return t1.Result.correlation;
        }
    }
}
