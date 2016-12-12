using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Wotan
{
    public sealed class nullLogger : logger
    {
        public nullLogger() : base(verbosity.high) {}

        protected override void addImpl(string message, logType t, int eventId = 0) {}
    }
}
