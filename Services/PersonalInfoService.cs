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
    public class PersonalInfoService : Service
    {

        public List<PersonalInfoModel> CreateModels()
        {
#if DEBUG
            Trace.WriteLine(message: "Create Personal Info Models");
#endif

            List<PersonalInfoModel> list = new List<PersonalInfoModel>();
            XmlDocument doc = LoadXml(filename: @"PersonalInfo.xml");

            //parse xml into models
            if (doc.DocumentElement != null)
            {
                foreach (XmlNode node in doc.DocumentElement.ChildNodes)
                {
                    XmlNode nameNode = node.SelectSingleNode(xpath: "Name");
                    XmlNode dataNode = node.SelectSingleNode(xpath: "Data");
                    if (nameNode == null || dataNode == null)
                    {
                        throw new Exception(message: "Invalid data in personal xml");
                    }
                    list.Add(item: new PersonalInfoModel(name: nameNode.InnerText, data: dataNode.InnerText));
                }
            }
            else
            {
                throw new NullReferenceException(message: "Unable to load xml file");
            }

            return list;
        }

        public string LoadDescriptionData(string field)
        {
#if DEBUG
            Trace.WriteLine(message: "Pull Description for " + field);
#endif
            string result = "";
            XmlDocument doc = LoadXml(filename: @"InfoPageDescriptions.xml");
            if (doc.DocumentElement != null)
            {
                foreach (XmlNode node in doc.DocumentElement.ChildNodes)
                {
                    XmlNode dataNode = node.SelectSingleNode(xpath: field);
                    if (dataNode == null)
                    {
                        throw new Exception(message: "Invalid data in personal xml");
                    }
                    return dataNode.InnerText;
                }
            }
            else
            {
                throw new NullReferenceException(message: "Unable to load xml file");
            }
            return result;
        }
        
    }
}
