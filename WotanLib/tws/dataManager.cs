/* Copyright (C) 2013 Interactive Brokers LLC. All rights reserved.  This code is subject to the terms
 * and conditions of the IB API Non-Commercial License or the IB API Commercial License, as applicable. */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IBApi;

namespace Wotan
{
    public abstract class dataManager<T> where T : messageType
    {
        protected client client_;

        protected delegate void updateCallback(message<T> msg);

        public dataManager(client client)
        {
            client_ = client;
        }

        public abstract void notifyError(int requestId);

        public abstract void clear();
    }
}
