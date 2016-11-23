using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wotan
{
    public class connectionStatusMessage : message<connectionStatus>
    {
        private bool isConnected;

        public bool IsConnected
        {
            get { return isConnected; }
        }

        public void connectionStatus(bool isConnected)
        {
            this.isConnected = isConnected;
        }
    }
}
