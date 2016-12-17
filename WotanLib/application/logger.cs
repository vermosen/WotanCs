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
        public void add(string message, logType t, verbosity v,int eventId = 0)
        {
            if (v >= threshold_) addImpl(message, t, eventId);
        }

        protected abstract void addImpl(string message, logType t, int eventId);
    }
}
