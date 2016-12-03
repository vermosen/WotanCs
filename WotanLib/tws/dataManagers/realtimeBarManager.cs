/* Copyright (C) 2013 Interactive Brokers LLC. All rights reserved.  This code is subject to the terms
 * and conditions of the IB API Non-Commercial License or the IB API Commercial License, as applicable. */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IBApi;

namespace Wotan
{
    public class realTimeBarsManager : dataManager
    {
        public realTimeBarsManager(client client, correlationManager corr) : base(client, corr)
        {
            client_.dispatcher.register(
                new Tuple<messageType, updateDelegate>[]
                {
                    new Tuple<messageType, updateDelegate>(messageType.realTimeBars, update)
                });
        }

        public override void update(message message)
        {
            throw new NotImplementedException();
        }

        public void addRequest(Contract contract, string whatToShow, bool useRTH)
        {
            client_.socket.reqRealTimeBars(corr_.next().id, contract, 5, whatToShow, useRTH, null);
        }

        public override void clear()
        {
            //client_.socket.cancelRealTimeBars(/*currentTicker + */1);
        }

        public override void notifyError(int requestId)
        {
            throw new NotImplementedException();
        }
    }
}
