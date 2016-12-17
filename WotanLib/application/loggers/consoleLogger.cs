using System;
using System.Threading;

namespace Wotan
{
    public sealed class consoleLogger : logger
    {
        private Mutex m_;

        public consoleLogger(verbosity threshold = verbosity.low) : base(threshold)
        {
            m_ = new Mutex();
        }

        protected override void addImpl(string message, logType t, int eventId = 0)
        {
            lock (m_)
            {
                // log in the log
                switch (t)
                {
                    case logType.error:
                        {
                            Console.WriteLine("[ERR] {0}", message);
                            break;
                        }
                    case logType.info:
                        {
                            Console.WriteLine("[INF] {0}", message);
                            break;
                        }
                    case logType.warning:
                        {
                            Console.WriteLine("[WAR] {0}", message);
                            break;
                        }
                    default:
                        {
                            throw new Exception("undefined message type");
                        }
                }
            }
        }
    }
}
