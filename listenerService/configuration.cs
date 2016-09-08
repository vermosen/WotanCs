using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace Wotan
{
    [Serializable]
    public class configuration
    {
        [Serializable]
        public class IBCredentials
        {
            private string  login_          ;
            private string  password_       ;
            private int     port_           ;
            private int     controllerPort_ ;
            private string  path_           ;
            private int     maxAttempt_     ;
            private int     delayAttempt_   ;

            [XmlElement("login")]
            public string login
            {
                get { return login_; }
                set { login_ = value; }
            }

            [XmlElement("password")]
            public string password
            {
                get { return password_; }
                set { password_ = value; }
            }

            [XmlElement("port")]
            public int port
            {
                get { return port_; }
                set { port_ = value; }
            }

            [XmlElement("controllerPort")]
            public int controllerPort
            {
                get { return controllerPort_; }
                set { controllerPort_ = value; }
            }

            [XmlElement("path")]
            public string path
            {
                get { return path_; }
                set { path_ = value; }
            }

            [XmlElement("maxAttempt")]
            public int maxAttempt
            {
                get { return maxAttempt_; }
                set { maxAttempt_ = value; }
            }

            [XmlElement("delayAttempt")]
            public int delayAttempt
            {
                get { return delayAttempt_; }
                set { delayAttempt_ = value; }
            }
        }

        // fields
        private IBCredentials interactiveBroker_;

        [XmlElement("interactiveBroker")]
        public IBCredentials interactiveBroker
        {
            get { return interactiveBroker_; }
            set { interactiveBroker_ = value; }
        }
    }
}
