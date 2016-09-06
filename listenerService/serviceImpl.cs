using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wotan
{
    // service implementation
    public class serviceImpl : service
    {
        private configuration config_;

        public serviceImpl(logger log) : base(log) { }
        public override void onStartImpl()
        {
            Console.WriteLine("Hello {0}", config_.test);
        }
        public override void onStopImpl()
        {
            Console.WriteLine("Goodbye !");
        }
        public override void loadPreferencesImpl(string xmlPath)
        {
            config_ = (new xmlParser<configuration>()).ToObject(new FileStream(xmlPath, FileMode.Open));
        }

        // for designer only
        [Obsolete] private serviceImpl() : base(null) {}
    }
}
