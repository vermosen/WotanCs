using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Wotan
{
    internal enum shutdownReason
    {
        PressCtrlC = 0,
        PressCtrlBreak = 1,
        ConsoleClosing = 2,
        WindowsLogOff = 5,
        WindowsShutdown = 6,
        ReachEndOfMain = 1000,
        Exception = 1001
    }
    internal class shutdownEventArgs
    {
        public readonly Exception ex_;
        public readonly shutdownReason reason_;

        public shutdownEventArgs(shutdownReason reason)
        {
            reason_ = reason;
            ex_     = null  ;
        }

        public shutdownEventArgs(Exception exception)
        {
            reason_ = shutdownReason.Exception;
            ex_ = exception;
        }
    }

    class Program
    {
        private delegate bool kernel32ShutdownHandler(shutdownReason reason);

        [DllImport("Kernel32")]
        private static extern bool SetConsoleCtrlHandler(kernel32ShutdownHandler handler, bool add);

        // shutdown events

        protected static void raiseShutdownEvent(shutdownEventArgs args)
        {
            switch (args.reason_)
            {
                case shutdownReason.ConsoleClosing:
                case shutdownReason.PressCtrlC:
                case shutdownReason.PressCtrlBreak:
                case shutdownReason.WindowsLogOff:
                case shutdownReason.WindowsShutdown:
                    {
                        signal_.Set();
                        break;
                    }
                case shutdownReason.Exception:
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
            raiseShutdownEvent(new shutdownEventArgs(shutdownReason.ReachEndOfMain));
        }
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            raiseShutdownEvent(new shutdownEventArgs(e.ExceptionObject as Exception));
        }
        static bool Kernel32_ProcessShuttingDown(shutdownReason sig)
        {
            raiseShutdownEvent(new shutdownEventArgs(sig));
            return false;
        }

        // reset signal
        protected static ManualResetEvent signal_ = new ManualResetEvent(false);

        static int Main(string[] args)
        {
            // set handlers
            SetConsoleCtrlHandler(new kernel32ShutdownHandler(Kernel32_ProcessShuttingDown), true);
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            try
            {
                serviceImpl srv = new serviceImpl(new winLogger("Wotan", "Program Startup", verbosity.high));
                srv.startDebug(new string[] { @"-xml=listener_uat.xml" });
                signal_.WaitOne();
                srv.stopDebug();
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("an unexpected error has occurred: {0}.{1}Press any Key...", ex.Message, Environment.NewLine);
                Console.ReadKey();
                return 1;
            }
        }
    }
}
