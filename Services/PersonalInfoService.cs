using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Models;

namespace Services
{
    public class PersonalInfoService
    {

        private XmlDocument LoadXml()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filename: @"PersonalInfo.xml");
            if (doc == null)
            {
                throw new Exception(message: "Cannot find xml document");
            }
            return doc;
        }

        public List<PersonalInfoModel> CreateModels()
        {
#if DEBUG
            Trace.WriteLine(message: "CreateModels");
#endif

            List<PersonalInfoModel> list = new List<PersonalInfoModel>();
            XmlDocument doc = LoadXml();

            //parse xml into models
            if (doc.DocumentElement != null)
                foreach (XmlNode node in doc.DocumentElement.ChildNodes)
                {
                    XmlNode nameNode = node.SelectSingleNode(xpath: "Name");
                    XmlNode dataNode = node.SelectSingleNode(xpath: "Data");
                    if (nameNode == null || dataNode == null)
                    {
                        throw new Exception(message: "Invalid data in xml");
                    }
                    list.Add(item: new PersonalInfoModel(name: nameNode.InnerText, data: dataNode.InnerText));
                }
            return list;
        }
        
    }
}
