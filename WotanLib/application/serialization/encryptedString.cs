using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Wotan
{
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
        public encryptedString() { }

        public encryptedString(string decrypted, bool encrypt = true, encrypterType method = encrypterType.AES)
        {
            this.encrypt = encrypt;
            this.method = method;
            this.decrypted = decrypted;
        }
    }
}
