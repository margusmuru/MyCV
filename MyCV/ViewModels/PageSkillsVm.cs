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
    public class PageSkillsVm
    {
        private ObservableCollection<SkillsInfoModel> _skillsInfoModels;

        public ObservableCollection<SkillsInfoModel> SkillsInfoModels
        {
            get { return _skillsInfoModels; }
            set { _skillsInfoModels = value; }
        }

        public PageSkillsVm(PageSkills pageSkills)
        {
#if DEBUG
            Trace.WriteLine(message: "PageSkillsVm loaded");
#endif
            _skillsInfoModels = new ObservableCollection<SkillsInfoModel>();
            LoadSkillsInfo();
        }

        private void LoadSkillsInfo()
        {
#if DEBUG
            Trace.WriteLine(message: "LoadSkillsInfo");
#endif
            SkillsInfoService service = new SkillsInfoService();
            List<SkillsInfoModel> list = service.CreateModels();
            if (list == null)
            {
                throw new NullReferenceException(message: "Unable to load xml data from service");
            }
            foreach (var model in list)
            {
                _skillsInfoModels.Add(item: model);
            }
#if DEBUG
            Trace.WriteLine(message: "LoadedSkillsInfo count: " + _skillsInfoModels.Count);
#endif
        }
    }
}
