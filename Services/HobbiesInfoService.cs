using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Models;

namespace Services
{
    public class HobbiesInfoService : Service
    {
        public List<PersonalInfoModel> CreateModels()
        {

            List<PersonalInfoModel> list = new List<PersonalInfoModel>();
            XmlDocument doc = LoadXml(filename: @"HobbiesAndLinks.xml");


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
    }
}
