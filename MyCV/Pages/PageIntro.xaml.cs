using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
    /// Interaction logic for PageIntro.xaml
    /// </summary>
    public partial class PageIntro : Page
    {
        private readonly MainWindow _main;

        private readonly PageIntroVm _pageIntroVm;

        public PageIntro(MainWindow window)
        {
            InitializeComponent();
            this._main = window;
            _pageIntroVm = new PageIntroVm();
            this.DataContext = _pageIntroVm;
#if DEBUG
            Trace.WriteLine(message: "PageIntro xaml.cs loaded");
#endif

        }
        private void BtnEducation(object sender, RoutedEventArgs e)
        {
            _main.Content = new PageMineSweeper(window: _main);
        }

        private void BtnWorkExperience(object sender, RoutedEventArgs e)
        {
            _main.Content = new PageBlackJack(window: _main);
        }

        private void BtnSkills(object sender, RoutedEventArgs e)
        {
            _main.Content = new PageSkills(window: _main);
        }

        private void BtnHobbies(object sender, RoutedEventArgs e)
        {
            //_main.Content = new PageSkills(window: _main);
        }

    }
}
