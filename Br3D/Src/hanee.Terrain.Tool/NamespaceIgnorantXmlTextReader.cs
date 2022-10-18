using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace hanee.Road.Exchange
{
    public class NamespaceIgnorantXmlTextReader : XmlTextReader
    {
        public NamespaceIgnorantXmlTextReader(Stream reader) : base(reader) { }

        public override string NamespaceURI
        {
            get { return string.Empty; }
        }
    }
}
