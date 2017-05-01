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
    public class PageIntroVm
    {
        private ObservableCollection<PersonalInfoModel> _personalInfoModels;

        public ObservableCollection<PersonalInfoModel> PersonalInfoModels
        {
            get { return _personalInfoModels; }
            set { _personalInfoModels = value; }
        }

        public PageIntroVm()
        {
#if DEBUG
            Trace.WriteLine(message: "PageIntroVm loaded");
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
            if (list == null)
            {
                throw new NullReferenceException(message: "Unable to load xml data from service");
            }
            foreach (var item in list)
            {
                _personalInfoModels.Add(item: item);
            }
        }

    }
}
