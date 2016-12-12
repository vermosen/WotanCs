using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
