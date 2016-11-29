using System;
using System.ServiceProcess;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;

namespace Wotan
{
    // base class for windows service with log and 
    public abstract class service : ServiceBase
    {
        // default log
        protected logger log_ = new winLogger("Application", "Application", verbosity.low);

        public service(string[] args)
        {
            List<Tuple<string, logType, verbosity, int>> temp = new List<Tuple<string, logType, verbosity, int>>();

            try
            {
                foreach (var arg in args)
                {
                    string[] s = arg.Split('=');

                    if (s.Length != 2)
                        temp.Add(new Tuple<string, logType, verbosity, int>(
                            "invalid argument format passed: " + arg,
                            logType.warning, verbosity.high, 0));

                    switch (s[0].ToUpper())
                    {
                        case "-XML":
                            {
                                temp.Add(new Tuple<string, logType, verbosity, int>(
                                    "detected setting switch: " + arg,
                                    logType.info, verbosity.low, 0));

                                FileInfo fi = null;                                                 // check for filename validity

                                try
                                {
                                    fi = new FileInfo(s[1]);
                                }
                                catch (ArgumentException ex)
                                {
                                    temp.Add(new Tuple<string, logType, verbosity, int>(
                                        "invalid setting file " + ex.ToString(),
                                        logType.error, verbosity.high, 0));
                                }
                                catch (PathTooLongException ex)
                                {
                                    temp.Add(new Tuple<string, logType, verbosity, int>(
                                        "invalid setting path " + ex.ToString(),
                                        logType.error, verbosity.high, 0));
                                }
                                catch (NotSupportedException ex)
                                {
                                    temp.Add(new Tuple<string, logType, verbosity, int>(
                                        "invalid setting file " + ex.ToString(),
                                        logType.error, verbosity.high, 0));
                                }

                                if (ReferenceEquals(fi, null))                                      // control the file path validity
                                    temp.Add(new Tuple<string, logType, verbosity, int>(
                                        "settings path format is not valid",
                                        logType.error, verbosity.high, 0));
                                else
                                {
                                    if (!File.Exists(s[1]))
                                        temp.Add(new Tuple<string, logType, verbosity, int>(
                                            "settings file not found",
                                            logType.error, verbosity.high, 0));
                                    else
                                    {
                                        loadPreferencesImpl(s[1]);
                                    }
                                }

                                break;
                            }
                        case "-D":
                        case "-DEBUG":
                            {
                                temp.Add(new Tuple<string, logType, verbosity, int>(
                                    "detected debugger switch: " + arg,
                                    logType.info, verbosity.low, 0));

                                Debugger.Launch();
                                break;
                            }
                        default:
                            {
                                temp.Add(new Tuple<string, logType, verbosity, int>(
                                    "unknown switch will be ignored: " + arg,
                                    logType.warning, verbosity.low, 0));
                                break;
                            }
                    }
                }
            }
            catch (Exception ex)
            {
                temp.Add(new Tuple<string, logType, verbosity, int>(
                    ex.ToString(), logType.error, verbosity.high, 0));
            }
            finally
            {
                if (log_ != null)
                {
                    foreach (var t in temp)
                    {
                        log_.add(t.Item1, t.Item2, t.Item3, t.Item4);
                    }
                }
            }

            InitializeComponent();
        }

        // interfaces
        public abstract void onStartImpl(string[] args);
        public abstract void onStopImpl();
        public abstract void loadPreferencesImpl(string xmlPath);
        protected virtual void InitializeComponent() {}

        protected override void OnStart(string[] args)
        {
            try
            {
                onStartImpl(args);
            }
            catch (Exception ex)
            {
                log_.add("an error has occurred: " + ex.Message + Environment.NewLine + 
                         "Shutting down the service...", logType.error, verbosity.high);
                Stop();
            }
        }

        protected override void OnStop()
        {
            try
            {
                onStopImpl();
            }
            catch (Exception ex)
            {
                log_.add("an error has occurred while shutting down the service: " + ex.Message,
                    logType.error, verbosity.high);
            }
            
        }

        #if DEBUG
        public void startDebug(string[] args)
        {
            OnStart(args);
        }

        public void stopDebug()
        {
            OnStop();
        }
        #endif
    }
}
