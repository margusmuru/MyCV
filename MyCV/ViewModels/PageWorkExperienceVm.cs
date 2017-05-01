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
    public class PageWorkExperienceVm
    {
        private ObservableCollection<WorkExperienceInfoModel> _workExperienceInfoModels;

        public ObservableCollection<WorkExperienceInfoModel> WorkExperienceInfoModels      
        {
            get { return _workExperienceInfoModels; }
            set { _workExperienceInfoModels = value; }
        }

        public PageWorkExperienceVm(PageWorkExperience pageWorkExperience)
        {
#if DEBUG
            Trace.WriteLine(message: "PageWorkExperienceVm loaded");
#endif
            _workExperienceInfoModels
                = new ObservableCollection<WorkExperienceInfoModel>();
            LoadWorkExperienceInfo();
        }

        private void LoadWorkExperienceInfo()
        {
#if DEBUG
            Trace.WriteLine(message: "LoadWorkExperienceInfo");
#endif
            WorkExperienceService service = new WorkExperienceService();
            List<WorkExperienceInfoModel> list = service.CreateModels();
            if (list == null)
            {
                throw new NullReferenceException(message: "Unable to load xml data from service");
            }
            foreach (var model in list)
            {
                _workExperienceInfoModels.Add(item: model);
            }
#if DEBUG
            Trace.WriteLine(message: "LoadedWorkExperienceInfo count: " + _workExperienceInfoModels.Count);
#endif
        }
    }
}
