using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Wotan
{
    public partial class listenerService : serviceImpl
    {
        public listenerService(logger log) : base(log) {}
    }
}
