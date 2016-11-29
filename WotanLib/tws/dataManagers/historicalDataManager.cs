using IBApi;
using System;
using System.Collections.Generic;

namespace Wotan
{
    public class historicalDataManager : dataManager
    {
        public const int HISTORICAL_ID_BASE = 30000000;

        public historicalDataManager(client ibClient) : base(ibClient)
        {
            client_.dispatcher.register(
                new Tuple<messageType, updateDelegate>[]
                {
                    new Tuple<messageType, updateDelegate>(messageType.historicalData, update),
                    new Tuple<messageType, updateDelegate>(messageType.historicalDataEnd, end)
                });
        }

        public void addRequest(Contract contract, string endDateTime, string durationString, string barSizeSetting, string whatToShow, int useRTH, int dateFormat)
        {
            client_.socket.reqHistoricalData(   /*currentTicker + */HISTORICAL_ID_BASE,
                                                contract, endDateTime,
                                                durationString,
                                                barSizeSetting,
                                                whatToShow, useRTH,
                                                1, new List<TagValue>());
        }

        public override void update(message message)
        {
            //
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
