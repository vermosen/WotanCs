using Akka.Actor;
using IBApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wotan.actors
{
    public class historicalDataManager : dataManager, IHandle<IMessage>
    {
        public class request : IMessage
        {
            public Contract contract { get; private set; }
            public DateTime datetime { get; private set; }
            public TimeSpan ts1 { get; private set; }
            public TimeSpan ts2 { get; private set; }
            public barType type { get; private set; }
            public int i1 { get; private set; }
            public int i2 { get; private set; }

            public request(Contract contract, DateTime datetime, TimeSpan ts1, TimeSpan ts2, barType type, int i1, int i2)
            {
                this.contract = contract;
                this.datetime = datetime;
                this.ts1 = ts1;
                this.ts2 = ts2;
                this.type = type;
                this.i1 = i1;
                this.i2 = i2;
            }
        }

        public enum barType
        {
            MIDPOINT = 1
        }
        
        public historicalDataManager(IActorRef client, IActorRef corr, IActorRef logger) : base(client, corr, logger)
        {
        }

        public void Handle(IMessage message)
        {
            throw new NotImplementedException();
        }

        public static Props Props(IActorRef client_, IActorRef logger_)
        {
            throw new NotImplementedException();
        }
    }
}
