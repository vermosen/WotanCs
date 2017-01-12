namespace Wotan
{
    public abstract class encrypter
    {
        public abstract string encrypt(string s);
        public abstract string decrypt(string s);
    }

    public enum encrypterType
    {
        AES = 1,
        unknown = 0
    }

    public class encrypterFactory : factory<encrypterType, encrypter>
    {
        public encrypterFactory()
        {
            add<AESEncrypter>(encrypterType.AES);
        }
    }
}
