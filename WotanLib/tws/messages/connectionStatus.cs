using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wotan
{
    public class connectionStatus : message
    {
        public connectionStatus(bool isConnected) : base(messageType.connectionStatus)
        {
            this.isConnected = isConnected;
        }

        public bool isConnected
        {
            get;
            private set;
        }
    }
}
