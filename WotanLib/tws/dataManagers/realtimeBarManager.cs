/* Copyright (C) 2013 Interactive Brokers LLC. All rights reserved.  This code is subject to the terms
 * and conditions of the IB API Non-Commercial License or the IB API Commercial License, as applicable. */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IBApi;

namespace Wotan
{
    public class realTimeBarsManager : dataManager<realTimeBars>
    {
        public const int RT_BARS_ID_BASE = 40000000;

        public realTimeBarsManager(client ibClient) : base(ibClient)
        {
        }

        public void addRequest(Contract contract, string whatToShow, bool useRTH)
        {
            client_.requestRealTimeBars(this, 1, contract, 5, whatToShow, useRTH, null);
        }

        public override void clear()
        {
            client_.socket.cancelRealTimeBars(/*currentTicker + */RT_BARS_ID_BASE);
        }

        public override void notifyError(int requestId)
        {
            throw new NotImplementedException();
        }
    }
}
