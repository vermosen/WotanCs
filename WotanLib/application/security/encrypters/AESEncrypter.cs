using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Wotan
{
    public sealed class AESEncrypter : encrypter
    {
        private static byte[] key_ = { 123, 112, 19, 11, 24, 26, 85, 45, 114, 184, 27, 162, 37, 112, 254, 209, 241,
                                        24, 175, 144, 173, 53, 196, 29, 24, 26, 17, 114, 131, 236, 53, 209 };
        private static byte[] vector_ = { 146, 164, 191, 111, 23, 3, 113, 119, 231, 121, 221, 23, 79, 32, 114, 156 };
        private ICryptoTransform encryptor_, decryptor_;
        private Encoding encoder_;

        public AESEncrypter()
        {
            SymmetricAlgorithm rm = new RijndaelManaged();
            rm.Padding = PaddingMode.PKCS7;
            rm.Key = key_;
            rm.IV = vector_;
            encryptor_ = rm.CreateEncryptor();
            decryptor_ = rm.CreateDecryptor();
            encoder_ = new UTF8Encoding();
        }

        public override string decrypt(string s)
        {
            return encoder_.GetString(Transform(Convert.FromBase64String(s), decryptor_));
        }

        public override string encrypt(string s)
        {
            return Convert.ToBase64String(Transform(encoder_.GetBytes(s), encryptor_));
        }

        private byte[] Transform(byte[] s, ICryptoTransform transform)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(stream, transform, CryptoStreamMode.Write))
                {
                    cs.Write(s, 0, s.Length);
                    cs.FlushFinalBlock();
                }
                return stream.ToArray();
            }
        }
    }
}
