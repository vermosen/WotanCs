using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Wotan.application.security
{
    class securedString : IDisposable, IXmlSerializable
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public XmlSchema GetSchema()
        {
            throw new NotImplementedException();
        }

        public void ReadXml(XmlReader reader)
        {
            throw new NotImplementedException();
        }

        public void WriteXml(XmlWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
