using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class PersonalInfoModel
    {
        private string _name;
        public string Name
        {
            get { return _name + ": "; }
            //set { _name = value; }
        }

        public string Data { get;
            //set { _data = value; }
        }


        public PersonalInfoModel(string name, string data)
        {
            _name = name;
            Data = data;
        }
    }
}
