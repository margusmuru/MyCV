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
            set { _name = value; }
        }


        private string _data;

        public string Data
        {
            get { return _data; }
            set { _data = value; }
        }


        public PersonalInfoModel(string name, string data)
        {
            this._name = name;
            this._data = data;
        }
    }
}
