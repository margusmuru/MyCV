using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Services
{
    public class Service
    {
        public XmlDocument LoadXml(string filename)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filename: filename);
            if (doc == null)
            {
                throw new Exception(message: "Cannot find xml document");
            }
            return doc;
        }
    }
}
