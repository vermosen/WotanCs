using System;
using System.Diagnostics;

namespace Wotan
{
    public enum messageType
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
        public logger(verbosity threshold)
        {
            threshold_ = threshold;
        }
        public void log(string message, verbosity v, messageType t)
        {
            if (v >= threshold_) logImpl(message, t);
        }

        public abstract void logImpl(string message, messageType t);
    }

    public sealed class winLogger : logger
    {
        string log_; string source_;
        public winLogger(string log, string source, verbosity threshold) : base(threshold)
        {
            if (!EventLog.SourceExists(source))
                EventLog.CreateEventSource(source, log);

            log_ = log;
            source_ = source;
        }

        public override void logImpl(string message, messageType t)
        {
            // log in the log
            switch (t)
            {
                case messageType.error:
                    {
                        EventLog.WriteEntry(source_, log_, EventLogEntryType.Error);
                        break;
                    }
                case messageType.info:
                    {
                        EventLog.WriteEntry(source_, log_, EventLogEntryType.Information);
                        break;
                    }
                case messageType.warning:
                    {
                        EventLog.WriteEntry(source_, log_, EventLogEntryType.Warning);
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
