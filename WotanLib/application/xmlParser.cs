using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Wotan
{
    public class xmlParser<T> where T : class, new()
    {
        public T ToObject(Stream xml)
        {
            return (new XmlSerializer(typeof(T))).Deserialize(xml) as T;
        }
    }
}
