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
    /// Interaction logic for PageEducation.xaml
    /// </summary>
    public partial class PageEducation : Page
    {
        private readonly MainWindow _main;
        private readonly PageEducationVm _pageEducationVm;
        public PageEducation(MainWindow window)
        {
            InitializeComponent();
            this._main = window;
            _pageEducationVm = new PageEducationVm(pageEducation: this);
            this.DataContext = _pageEducationVm;
        }

        private void BtnClick_Back(object sender, RoutedEventArgs e)
        {
            _main.Content = new PageIntro(window: _main);
        }
    }
}
