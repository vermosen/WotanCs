using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Wotan
{
    public class xmlSerializer<T> : serializer<T> where T : class, new()
    {
        public override T toObject(Stream xml)
        {
            return (new XmlSerializer(typeof(T))).Deserialize(xml) as T;
        }
        public override Stream fromObject(T obj)
        {
            Stream s = new MemoryStream();

            (new XmlSerializer(typeof(T))).Serialize(s, obj);

            s.Position = 0;
            return s;
        }
    }
}
