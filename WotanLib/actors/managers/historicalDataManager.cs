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
            public correlation<int> correlation { get; set; }
            public Contract contract { get; private set; }
            public DateTime endDatetime { get; private set; }
            public TimeSpan duration { get; private set; }
            public TimeSpan barSize { get; private set; }
            public barType type { get; private set; }
            public int useRTH { get; private set; } // todo: create class/enum
            public int formatDate { get; private set; }

            public request(Contract contract, DateTime endDatetime, TimeSpan duration, TimeSpan barSize, barType type, int useRTH, int formatDate)
            {
                this.contract = contract;
                this.endDatetime = endDatetime;
                this.duration = duration;
                this.barSize = barSize;
                this.type = type;
                this.useRTH = useRTH;
                this.formatDate = formatDate;
            }
        }

        public enum barType
        {
            MIDPOINT = 1
        }
        
        public historicalDataManager(IActorRef client, IActorRef corr, IActorRef logger) : base(client, corr, logger) {}

        public void Handle(IMessage m)
        {
            if (m.GetType() == typeof(request))
            {
                var temp = (request)m;

                // we first insure the client is connected
                temp.correlation = Task.Run(async () =>
                {
                    var t = client_.Ask<correlationManager.reply>(new correlationManager.request(), TimeSpan.FromSeconds(1));
                    await t;
                    return t.Result.correlation;
                }).Result;

                if (Task.Run(async () =>
                 {
                     var t = corr_.Ask<connectionStatus>(new connectionStatus(), TimeSpan.FromSeconds(1));
                     await t;
                     return t.Result.isConnected;
                 }).Result == true)
                {
                    client_.Tell(m);
                }
            }
        }

        public static Props Props(IActorRef client, IActorRef corr, IActorRef logger)
        {
            return Akka.Actor.Props.Create(() => new historicalDataManager(client, corr, logger));
        }
    }
}
