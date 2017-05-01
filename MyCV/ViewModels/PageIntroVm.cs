using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Models;
using MyCV.Annotations;
using Services;

namespace MyCV.ViewModels
{
    public class PageIntroVm : INotifyPropertyChanged
    {
        private ObservableCollection<PersonalInfoModel> _personalInfoModels;
        public ObservableCollection<PersonalInfoModel> PersonalInfoModels
        {
            get { return _personalInfoModels; }
            set { _personalInfoModels = value; }
        }

        private string _workExperienceDescription;
        public string WorkExperienceDescription
        {
            get { return _workExperienceDescription; }
            set { _workExperienceDescription = value; OnPropertyChanged(propertyName: nameof(WorkExperienceDescription)); }
        }

        private string _skillsDescription;
        public string SkillsDescription
        {
            get { return _skillsDescription; }
            set { _skillsDescription = value; OnPropertyChanged(propertyName: nameof(SkillsDescription)); }
        }

        private string _hobbiesDescription;
        public string HobbiesDescription
        {
            get { return _hobbiesDescription; }
            set { _hobbiesDescription = value; OnPropertyChanged(propertyName: nameof(HobbiesDescription)); }
        }

        private string _educationDescription;
        public string EducationDescription
        {
            get { return _educationDescription; }
            set { _educationDescription = value; OnPropertyChanged(propertyName: nameof(EducationDescription)); }
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
            _educationDescription = service.LoadDescriptionData(field: "Education");
            _hobbiesDescription = service.LoadDescriptionData(field: "Hobbies");
            _skillsDescription = service.LoadDescriptionData(field: "Skills");
            _workExperienceDescription = service.LoadDescriptionData(field: "WorkExperience");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
