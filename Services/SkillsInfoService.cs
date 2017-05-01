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
    public class SkillsInfoService : Service
    {
        public List<SkillsInfoModel> CreateModels()
        {
#if DEBUG
            Trace.WriteLine(message: "Create Skills Info Models");
#endif
            List<SkillsInfoModel> list = new List<SkillsInfoModel>();
            XmlDocument doc = LoadXml(filename: @"skillsInfo.xml");

            //parse xml into models
            if (doc.DocumentElement != null)
            {
                foreach (XmlNode node in doc.DocumentElement.ChildNodes)
                {
                    XmlNode nameNode = node.SelectSingleNode(xpath: "Name");
                    XmlNode timeNode = node.SelectSingleNode(xpath: "Time");
                    XmlNode infoNode = node.SelectSingleNode(xpath: "Info");
#if DEBUG
                    Trace.WriteLine(message: nameNode.InnerText);
                    Trace.WriteLine(message: timeNode.InnerText);
                    Trace.WriteLine(message: infoNode.InnerText);
#endif
                    if (nameNode == null || timeNode == null || infoNode == null)
                    {
                        throw new Exception(message: "Invalid data in skills xml");
                    }
                    list.Add(item: new SkillsInfoModel());
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
