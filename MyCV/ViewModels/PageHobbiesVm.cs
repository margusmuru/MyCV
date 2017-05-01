using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Models;
using MyCV.Annotations;
using MyCV.Pages;
using Services;

namespace MyCV.ViewModels
{
    public class PageHobbiesVm : INotifyPropertyChanged
    {
        private readonly PageHobbies _pageHobbies;

        private string _longText;
        public string LongText 
        {
            get { return _longText; }
            set { _longText = value; OnPropertyChanged(propertyName: nameof(LongText)); }
        }

        private string _longTextDes;
        public string LongTextDes
        {
            get { return _longTextDes; }
            set { _longTextDes = value; OnPropertyChanged(propertyName: nameof(LongTextDes)); }
        }

        private string _link1;
        public string Link1
        {
            get { return _link1; }
            set { _link1 = value; OnPropertyChanged(propertyName: nameof(Link1)); }
        }

        private string _link1Des;
        public string Link1Des
        {
            get { return _link1Des; }
            set { _link1Des = value; OnPropertyChanged(propertyName: nameof(Link1Des)); }
        }

        private string _link2;
        public string Link2
        {
            get { return _link2; }
            set { _link2 = value; OnPropertyChanged(propertyName: nameof(Link2)); }
        }

        private string _link2Des;
        public string Link2Des
        {
            get { return _link2Des; }
            set { _link2Des = value; OnPropertyChanged(propertyName: nameof(Link2Des)); }
        }



        public PageHobbiesVm(PageHobbies pageHobbies)
        {
            _pageHobbies = pageHobbies;
            LoadHobbiesInfo();
        }

        private void LoadHobbiesInfo()
        {
            HobbiesInfoService service = new HobbiesInfoService();
            List<PersonalInfoModel> list = service.CreateModels();
            if (list == null)
            {
                throw new NullReferenceException(message: "Unable to load xml data from service");
            }

            LongTextDes = list[index: 0].Name;
            LongText = list[index: 0].Data;

            Link1Des = list[index: 1].Name;
            Link1 = list[index: 1].Data;

            Link2Des = list[index: 2].Name;
            Link2 = list[index: 2].Data;
        }






        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
