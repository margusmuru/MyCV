using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class EducationInfoModel
    {
        public string Name { get;
            //set { _name = value; }
        }

        public string Time { get;
            //set { _time = value; }
        }

        public string Info { get;
            //set { _info = value; }
        }

        public EducationInfoModel(string name, string time, string info)
        {
            Name = name;
            Time = time;
            Info = info;
        }

        public EducationInfoModel()
        {
            
        }

    }
}
