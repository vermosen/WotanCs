using Akka.Actor;
using IBApi;
using System;
using System.Collections.Generic;

namespace Wotan
{
    public class historicalDataManager : dataManager
    {
        private List<historicalData> data_;

        public historicalDataManager(client ibClient, IActorRef corr) : base(ibClient, corr)
        {
            //client_.dispatcher.register(
            //    new Tuple<messageType, updateDelegate>[]
            //    {
            //        new Tuple<messageType, updateDelegate>(messageType.historicalData, update),
            //        new Tuple<messageType, updateDelegate>(messageType.historicalDataEnd, end)
            //    });

            data_ = new List<historicalData>();
        }

        public void addRequest(Contract contract, string endDateTime, string durationString, string barSizeSetting, string whatToShow, int useRTH, int dateFormat)
        {
            //corr_.next();
            client_.socket.reqHistoricalData(   /*corr_.next().id*/0,
                                                contract, endDateTime,
                                                durationString,
                                                barSizeSetting,
                                                whatToShow, useRTH,
                                                1, new List<TagValue>());
        }

        public override void update(message message)
        {
            data_.Add(message as historicalData);
        }

        public void end(message message)
        {
            
        }

        public override void clear() {}

        public override void notifyError(int requestId)
        {
            throw new NotImplementedException();
        }
    }
}
