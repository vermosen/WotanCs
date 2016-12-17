using System.ServiceProcess;

namespace Wotan
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new listenerService(args)
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
