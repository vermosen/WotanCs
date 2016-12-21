using Akka.Actor;
using System;

namespace Wotan
{
    public class accountDataManager : dataManager
    {
        public accountDataManager(client ibClient, IActorRef corr) : base(ibClient, corr)
        {
            //client_.dispatcher.register(
            //    new Tuple<messageType, updateDelegate>[]
            //    {
            //        new Tuple<messageType, updateDelegate>(messageType.managedAccounts, update)
            //    });
        }

        public override void update(message message)
        {
            //
        }

        public void end(message message)
        {

        }

        public override void clear() { }

        public override void notifyError(int requestId)
        {
            throw new NotImplementedException();
        }
    }
}
