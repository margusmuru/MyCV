using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class WorkExperienceInfoModel : EducationInfoModel
    {
        private string _position;
        public string Position
        {
            get
            {
                return "Position: " + _position;
            }
        }

        public WorkExperienceInfoModel(string name, string time, string info, string position) 
            :base(name: name, time: time, info: info)
        {
            _position = position;
        }
    }
}
