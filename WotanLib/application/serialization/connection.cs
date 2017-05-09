using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Wotan
{
    [DataContract]
    public class connection
    {
        [DataMember(IsRequired = true, Name = "user", Order = 0)]
        public string user { get; private set; }

        [DataMember(IsRequired = true, Name = "password", Order = 1)]
        public encryptedString password { get; private set; }
    }
}