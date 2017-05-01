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
    public class WorkExperienceService : Service
    {
        public List<WorkExperienceInfoModel> CreateModels()
        {
#if DEBUG
            Trace.WriteLine(message: "Create work experience Info Models");
#endif
            List<WorkExperienceInfoModel> list = new List<WorkExperienceInfoModel>();
            XmlDocument doc = LoadXml(filename: @"WorkExperienceInfo.xml");

            //parse xml into models
            if (doc.DocumentElement != null)
            {
                foreach (XmlNode node in doc.DocumentElement.ChildNodes)
                {
                    XmlNode nameNode = node.SelectSingleNode(xpath: "Name");
                    XmlNode timeNode = node.SelectSingleNode(xpath: "Time");
                    XmlNode infoNode = node.SelectSingleNode(xpath: "Info");
                    XmlNode posNode = node.SelectSingleNode(xpath: "Position");
#if DEBUG
                    Trace.WriteLine(message: nameNode.InnerText);
                    Trace.WriteLine(message: timeNode.InnerText);
                    Trace.WriteLine(message: infoNode.InnerText);
                    Trace.WriteLine(message: posNode.InnerText);
#endif
                    if (nameNode == null || timeNode == null || infoNode == null || posNode == null)
                    {
                        throw new Exception(message: "Invalid data in work experience xml");
                    }
                    list.Add(item: new WorkExperienceInfoModel(
                        name: nameNode.InnerText,
                        time: timeNode.InnerText,
                        info: infoNode.InnerText,
                        position: posNode.InnerText));
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
