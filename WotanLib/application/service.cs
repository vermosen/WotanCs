using System;
using System.ServiceProcess;
using System.IO;

namespace Wotan
{
    // base class for windows service with log
    public abstract class service : ServiceBase
    {
        logger log_;
        public service(logger log)
        {
            log_ = log;

            InitializeComponent();
        }

        // interfaces
        public abstract void onStartImpl();
        public abstract void onStopImpl();
        public abstract void loadPreferencesImpl(string xmlPath);

        protected virtual void InitializeComponent() { }

        protected override void OnStart(string[] args)
        {
            try
            {
                if (args.Length != 1)                                               // check command argument format
                    throw new Exception("invalid argument size");

                args = args[0].Split('=');

                if (args.Length != 2)
                    throw new Exception("invalid argument format");

                if (args[0] != "-xml")
                    throw new Exception("invalid argument name");
                
                FileInfo fi = null;                                                 // check for filename validity
                try
                {
                    fi = new System.IO.FileInfo(args[1]);
                }
                catch (ArgumentException) { }
                catch (System.IO.PathTooLongException) { }
                catch (NotSupportedException) { }

                if (ReferenceEquals(fi, null))                                      // control the file path validity
                    throw new Exception("file path format is not valid");
                else
                {
                    if (!File.Exists(args[1]))
                        throw new Exception("file path does not exist");
                    else
                    {
                        loadPreferencesImpl(args[1]);
                    }
                }

                onStartImpl();
            }
            catch (Exception ex)
            {
                log_.log("an error has occurred: " + ex.Message + Environment.NewLine + 
                         "Shutting down the service...", verbosity.high, messageType.error);
                Stop();
            }
        }

        protected override void OnStop()
        {
            onStopImpl();
        }

        public void startDebug(string[] args)
        {
            OnStart(args);
        }

        public void stopDebug()
        {
            OnStop();
        }
    }
}
