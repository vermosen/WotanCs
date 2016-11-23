using System;
using System.Diagnostics;
using System.Threading;

namespace Wotan
{
    public enum logType
    {
        info        = 1,
        error       = 2,
        warning     = 3,
        undefined   = 0
    }

    public enum verbosity
    {
        low         = 1,
        medium      = 2,
        high        = 3,
        undefined   = 0
    }

    public abstract class logger
    {
        private verbosity threshold_;
        public logger(verbosity threshold = verbosity.low)
        {
            threshold_ = threshold;
        }
        public void log(string message, logType t, verbosity v,int eventId = 0)
        {
            if (v >= threshold_) logImpl(message, t, eventId);
        }

        protected abstract void logImpl(string message, logType t, int eventId);
    }

    public sealed class winLogger : logger
    {
        private string log_;
        private string source_;

        public winLogger(string log, string source, verbosity threshold = verbosity.low) : base(threshold)
        {
            if (!EventLog.SourceExists(source))
                EventLog.CreateEventSource(source, log);

            log_ = log;
            source_ = source;
        }

        protected override void logImpl(string message, logType t, int eventId = 0)
        {
            // log in the log
            switch (t)
            {
                case logType.error:
                    {
                        EventLog.WriteEntry(source_, log_, EventLogEntryType.Error, eventId);
                        break;
                    }
                case logType.info:
                    {
                        EventLog.WriteEntry(source_, log_, EventLogEntryType.Information, eventId);
                        break;
                    }
                case logType.warning:
                    {
                        EventLog.WriteEntry(source_, log_, EventLogEntryType.Warning, eventId);
                        break;
                    }
                default:
                    {
                        throw new Exception("undefined message type");
                    }
            }
        }
    }
    public sealed class consoleLogger : logger
    {
        private Mutex m_;

        public consoleLogger(verbosity threshold = verbosity.low) : base(threshold)
        {
            m_ = new Mutex();
        }

        protected override void logImpl(string message, logType t, int eventId = 0)
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
