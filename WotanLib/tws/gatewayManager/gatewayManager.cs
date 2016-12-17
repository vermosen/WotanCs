namespace Wotan
{
    // a window manager to start the gateway service
    public class gatewayManager
    {
        private logger log_;
        private interactiveBroker settings_;

        public gatewayManager(interactiveBroker settings, logger log)
        {
            settings_ = settings;
            log_ = log;
        }

        public bool connect()
        {
            return true;
        }
    }
}
