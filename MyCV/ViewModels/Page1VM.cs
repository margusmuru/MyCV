using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Services;

namespace MyCV.ViewModels
{
    public class Page1VM
    {
        private ObservableCollection<PersonalInfoModel> _personalInfoModels;

        public ObservableCollection<PersonalInfoModel> PersonalInfoModels
        {
            get { return _personalInfoModels; }
            set { _personalInfoModels = value; }
        }

        public Page1VM()
        {
#if DEBUG
            Trace.WriteLine(message: "Page1VM loaded");
#endif
            _personalInfoModels = new ObservableCollection<PersonalInfoModel>();
            LoadPersonalInfo();
        }

        private void LoadPersonalInfo()
        {
#if DEBUG
            Trace.WriteLine(message: "LoadPersonalInfo");
#endif
            PersonalInfoService service = new PersonalInfoService();
            List<PersonalInfoModel> list = service.CreateModels();

            foreach (var item in list)
            {
                _personalInfoModels.Add(item: item);
            }
        }

    }
}
