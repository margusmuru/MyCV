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
            XmlDocument doc = LoadXml(filename: @"SkillsInfo.xml");

            //parse xml into models
            if (doc.DocumentElement != null)
            {
                foreach (XmlNode node in doc.DocumentElement.ChildNodes)
                {
                    XmlNode infoNode = node.SelectSingleNode(xpath: "Name");
#if DEBUG
                    Trace.WriteLine(message: infoNode.InnerText);
#endif
                    if (infoNode == null)
                    {
                        throw new Exception(message: "Invalid data in skills xml");
                    }
                    list.Add(item: new SkillsInfoModel(name: infoNode.InnerText));
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
