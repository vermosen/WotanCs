using System;
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
    [DataContract]
    public class encryptedString
    {
        private encrypter encrypter_;
        private string decrypted_;

        [DataMember(IsRequired = true, Name = "encrypt", Order = 0)]
        private bool encrypt { get; set; }

        [DataMember(IsRequired = true, Name = "string", Order = 1)]
        private string encryptedStr { get; set; }

        [DataMember(IsRequired = false, Name = "method", Order = 2)]
        private encrypterType method { get; set; }

        public string decrypted
        {
            get
            {
                if (encrypt == true)
                {
                    if (encrypter_ == null)
                        encrypter_ = (new encrypterFactory()).create(method);

                    return encrypter_.decrypt(encryptedStr);
                }
                else return encryptedStr;
            }
            private set
            {
                decrypted_ = value;

                if (encrypter_ == null)
                    encrypter_ = (new encrypterFactory()).create(method);

                if (encrypt)
                    encryptedStr = encrypter_.encrypt(decrypted_);
                else
                    encryptedStr = decrypted_;
            }
        }

        // for serialization
        [Obsolete("for serialization only", true)]
        public encryptedString() {}

        public encryptedString(string decrypted, bool encrypt = true, encrypterType method = encrypterType.AES)
        {
            this.encrypt = encrypt;
            this.method = method;
            this.decrypted = decrypted;
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
