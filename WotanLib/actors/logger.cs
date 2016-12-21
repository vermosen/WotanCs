using Akka.Actor;

namespace Wotan.actors
{
    public class log
    {
        public log(string message, logType type, verbosity v, int eventId = 0)
        {
            this.message = message;
            this.type = type;
            this.verbosity = v;
            this.eventId = eventId;
        }
        public string message { get; private set; }
        public logType type { get; private set; }
        public verbosity verbosity { get; private set; }
        public int eventId { get; private set; }
    }

    public delegate void logDlg(log l);

    public class logger : TypedActor, IHandle<log>
    {
        private Wotan.logger logger_;

        public logger(Wotan.logger logger)
        {
            logger_ = logger;
        }

        public void Handle(log l)
        {
            logger_.add(l.message, l.type, l.verbosity, l.eventId);
        }
    }
}
