using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Wotan
{
    [Serializable()]
    public class configuration : ISerializable
    {
        [Obsolete]
        public configuration() {}                     // Empty constructor required to compile.
        
        // fields
        private string test_;

        public string test
        {
            get { return test_; }
            set { test_ = value; }
        }

        public configuration(SerializationInfo info, StreamingContext ctxt)
        {
            // Reset the property value using the GetValue method.
            test_ = (string)info.GetValue("test", typeof(string));
        }

        //Serialization function.
        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("props", test_, typeof(string));
        }
    }
}
