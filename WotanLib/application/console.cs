using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ipvTools
{
    // a console class with the ability
    // to host service process
    public class consoleBase
    {
        public class instance
        {
            public class shutdownEventArgs
            {
                public enum shutdownReason
                {
                    PressCtrlC = 0,
                    PressCtrlBreak = 1,
                    ConsoleClosing = 2,
                    WindowsLogOff = 5,
                    WindowsShutdown = 6,
                    ReachEndOfMain = 1000,
                    Exception = 1001
                }

                public readonly Exception ex_;
                public readonly shutdownReason reason_;

                public shutdownEventArgs(shutdownReason reason)
                {
                    reason_ = reason;
                    ex_ = null;
                }

                public shutdownEventArgs(Exception exception)
                {
                    reason_ = shutdownReason.Exception;
                    ex_ = exception;
                }
            }

            private delegate bool kernel32ShutdownHandler(shutdownEventArgs.shutdownReason reason);

            [DllImport("Kernel32")]
            private static extern bool SetConsoleCtrlHandler(kernel32ShutdownHandler handler, bool add);

            public static ManualResetEvent signal_ = new ManualResetEvent(false);

            public instance()
            {
                // set handlers
                SetConsoleCtrlHandler(new kernel32ShutdownHandler(Kernel32_ProcessShuttingDown), true);
                AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
                AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
                Thread.CurrentThread.Name = "Main";
            }

            public static void raiseShutdownEvent(shutdownEventArgs args)
            {
                switch (args.reason_)
                {
                    case shutdownEventArgs.shutdownReason.ConsoleClosing:
                    case shutdownEventArgs.shutdownReason.PressCtrlC:
                    case shutdownEventArgs.shutdownReason.PressCtrlBreak:
                    case shutdownEventArgs.shutdownReason.WindowsLogOff:
                    case shutdownEventArgs.shutdownReason.WindowsShutdown:
                        {
                            signal_.Set();
                            break;
                        }
                    case shutdownEventArgs.shutdownReason.Exception:
                        {
                            Console.WriteLine("an error has occurred: {0}", args.ex_);
                            signal_.Set();
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }

                signal_.Reset();
                signal_.WaitOne();
            }

            // handlers
            static void CurrentDomain_ProcessExit(object sender, EventArgs e)
            {
                raiseShutdownEvent(new shutdownEventArgs(shutdownEventArgs.shutdownReason.ReachEndOfMain));
            }
            static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
            {
                raiseShutdownEvent(new shutdownEventArgs(e.ExceptionObject as Exception));
            }
            static bool Kernel32_ProcessShuttingDown(shutdownEventArgs.shutdownReason sig)
            {
                raiseShutdownEvent(new shutdownEventArgs(sig));
                return false;
            }
        }

        static protected instance instance_ = new instance();
    }
}
