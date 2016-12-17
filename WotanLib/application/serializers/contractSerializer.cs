using System.IO;
using System.Runtime.Serialization;

namespace Wotan
{
    public class contractSerializer<T> : serializer<T> where T : class, new()
    {
        public override Stream fromObject(T obj)
        {
            Stream s = new MemoryStream();
            DataContractSerializer dcs = new DataContractSerializer(typeof(T));
            dcs.WriteObject(s, obj);
            s.Position = 0;
            return s;
        }

        public override T toObject(Stream xml)
        {
            DataContractSerializer dcs = new DataContractSerializer(typeof(T));
            return (T)dcs.ReadObject(xml);
        }
    }
}
