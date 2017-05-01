using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class WorkExperienceModel : EducationInfoModel
    {
        public string Position { get; }

        public WorkExperienceModel(string name, string time, string info, string position) 
            :base(name: name, time: time, info: info)
        {
            Position = position;
        }
    }
}
