using System;
using System.Diagnostics;

namespace Wotan
{
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

        protected override void addImpl(string message, logType t, int eventId = 0)
        {
            // log in the log
            switch (t)
            {
                case logType.error:
                    {
                        EventLog.WriteEntry(source_, message, EventLogEntryType.Error, eventId);
                        break;
                    }
                case logType.info:
                    {
                        EventLog.WriteEntry(source_, message, EventLogEntryType.Information, eventId);
                        break;
                    }
                case logType.warning:
                    {
                        EventLog.WriteEntry(source_, message, EventLogEntryType.Warning, eventId);
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
