using System.Net;
using System.Runtime.Serialization;

namespace Wotan
{
    public enum accountType
    {
        live = 1,
        paper = 2,
        undefined = 0
    }

    // TODO: replace with securedString
    public class encryptedString
    {
        [DataMember(IsRequired = true, Name = "encrypted", Order = 0)]
        private bool encrypted { get; set; }

        [DataMember(IsRequired = true, Name = "string", Order = 1)]
        private string encryptedStr { get; set; }

        public string decrypted
        {
            // TODO
            get { return encryptedStr; }
            set { encrypted = false; encryptedStr = value; }
        }
    }

    [DataContract]
    public class credentials
    {
        [DataMember(IsRequired = true, Name = "accountType", Order = 0)]
        public accountType accountType { get; set; }

        [DataMember(IsRequired = true, Name = "login", Order = 3)]
        public encryptedString login { get; set; }

        [DataMember(IsRequired = true, Name = "password", Order = 4)]
        public encryptedString password { get; set; }

        [DataMember(IsRequired = true, Name = "host", Order = 1)]
        private string _host { get; set; }

        [DataMember(IsRequired = true, Name = "port", Order = 2)]
        public int port { get; set; }

        public IPAddress host
        {
            get { return IPAddress.Parse(_host); }
            set { _host = value.ToString(); }
        }
    }

    [DataContract]
    public class interactiveBroker
    {
        [DataMember(IsRequired = true, Name = "application", Order = 0)]
        public string application { get; set; }

        [DataMember(IsRequired = true, Name = "credentials", Order = 1)]
        public credentials credentials { get; set; }

    }
}
