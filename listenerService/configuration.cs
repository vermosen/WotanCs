using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Wotan
{
    // TODO: use code contracts
    //[DataContract(Name = "configuration")]
    //public class configuration
    //{
    //    [DataContract(Name = "environment")]
    //    public class IBEnvironment
    //    {
    //        [DataMember(IsRequired = true, Name = "application", Order = 0)]
    //        public string application { get; private set; }
    //    }
    //}

    [Serializable]
    public class configuration
    {
        [XmlInclude(typeof(winLoggerSerializer))]
        [XmlInclude(typeof(consoleLoggerSerializer))]
        public abstract class loggerSerializer
        {
            [XmlElement("threshold", Type = typeof(verbosity), IsNullable = false)]
            public verbosity threshold { get; set; }

            public abstract logger create();
        }
        public class winLoggerSerializer : loggerSerializer
        {
            [XmlElement("log", Type = typeof(string), IsNullable = false)]
            public string log { get; set; }

            [XmlElement("source", Type = typeof(string), IsNullable = false)]
            public string source { get; set; }

            public override logger create() { return new winLogger(log, source, threshold); }
        }
        public class consoleLoggerSerializer : loggerSerializer
        {
            public override logger create() { return new consoleLogger(threshold); }
        }

        [Serializable]
        public class IBEnvironment
        {
            private string javaPath_;

            [XmlElement("javaPath")]
            public string javaPath
            {
                get { return javaPath_; }
                set { javaPath_ = value; }
            }
        }

        [Serializable]
        public class IBCredentials
        {
            [Serializable]
            public enum mode
            {
                [XmlEnum("live")]
                live        = 1,
                [XmlEnum("paper")]
                paper       = 2,
                undefined   = 0
            }

            private string  login_          ;
            private string  password_       ;
            private int     port_           ;
            private int     controllerPort_ ;
            private string  path_           ;
            private int     maxAttempt_     ;
            private int     delayAttempt_   ;
            private mode    mode_           ;

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

            [XmlElement("IBMode")]
            public mode IBMode
            {
                get { return mode_; }
                set { mode_ = value; }
            }
        }

        // fields
        private IBCredentials interactiveBroker_;
        private IBEnvironment environment_;

        [XmlElement("interactiveBroker")]
        public IBCredentials interactiveBroker
        {
            get { return interactiveBroker_; }
            set { interactiveBroker_ = value; }
        }
        [XmlElement("environment")]
        public IBEnvironment environment
        {
            get { return environment_; }
            set { environment_ = value; }
        }

        [XmlElement("logger", Type = typeof(loggerSerializer))] 
        public loggerSerializer logger { get; set; }
    }
}
