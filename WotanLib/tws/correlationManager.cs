using System;
using System.Threading;

namespace Wotan
{
    public class correlationManager
    {
        // members
        private int previous_;

        public correlationManager() { previous_ = 0; }

        public correlation<int> next()
        {
            return new correlation<int>(Interlocked.Increment(ref previous_));
        }
    }
}
