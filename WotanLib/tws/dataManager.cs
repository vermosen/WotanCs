/* Copyright (C) 2013 Interactive Brokers LLC. All rights reserved.  This code is subject to the terms
 * and conditions of the IB API Non-Commercial License or the IB API Commercial License, as applicable. */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IBApi;

namespace Wotan
{
    public abstract class dataManager //where T : message
    {
        protected client client_;
        protected correlationManager corr_;

        public dataManager(client client, correlationManager corr)
        {
            client_ = client;
            corr_ = corr;
        }

        public abstract void update(message message);
        public abstract void clear();
        public abstract void notifyError(int requestId);        
    }
}
