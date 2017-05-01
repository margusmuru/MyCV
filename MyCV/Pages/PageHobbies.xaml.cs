using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MyCV.ViewModels;

namespace MyCV.Pages
{
    /// <summary>
    /// Interaction logic for PageHobbies.xaml
    /// </summary>
    public partial class PageHobbies : Page
    {
        private readonly MainWindow _main;
        private readonly PageHobbiesVm _pageHobbiesVm;
        public PageHobbies(MainWindow window)
        {
            InitializeComponent();
            this._main = window;
            _pageHobbiesVm = new PageHobbiesVm(pageHobbies: this);
            this.DataContext = _pageHobbiesVm;
        }

        public void BtnClick_Back(object sender, RoutedEventArgs e)
        {
            _main.Content = new PageIntro(window: _main);
        }
    }
}
