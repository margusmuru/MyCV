using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using MyCV.Pages;
using Services;

namespace MyCV.ViewModels
{
    public class PageEducationVm
    {

        private ObservableCollection<EducationInfoModel> _educationInfoModels;

        public ObservableCollection<EducationInfoModel> EducationInfoModels
        {
            get { return _educationInfoModels; }
            set { _educationInfoModels = value; }
        }



        public PageEducationVm(PageEducation pageEducation)
        {
#if DEBUG
            Trace.WriteLine(message: "PageEducationVm loaded");
#endif
            _educationInfoModels = new ObservableCollection<EducationInfoModel>();
            LoadEducationInfo();
        }

        private void LoadEducationInfo()
        {
#if DEBUG
            Trace.WriteLine(message: "LoadEducationInfo");
#endif
            EducationInfoService service = new EducationInfoService();
            List<EducationInfoModel> list = service.CreateModels();
            if (list == null)
            {
                throw new NullReferenceException(message: "Unable to load xml data from service");
            }
            foreach (var model in list)
            {
                _educationInfoModels.Add(item: model);
            }
#if DEBUG
            Trace.WriteLine(message: "LoadedEducationInfo count: " + _educationInfoModels.Count);
#endif
        }

    }
}
