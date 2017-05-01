using System;
using System.Collections.Generic;
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
    /// Interaction logic for PageSkills.xaml
    /// </summary>
    public partial class PageSkills : Page
    {
        private readonly MainWindow _main;
        private readonly PageSkillsVm _pageSkillsVm;
        public PageSkills(MainWindow window)
        {
            InitializeComponent();
            this._main = window;
            _pageSkillsVm = new PageSkillsVm(pageSkills: this);
            this.DataContext = _pageSkillsVm;
#if DEBUG
            Trace.WriteLine(message: "PageSkills xaml.cs loaded");
#endif
        }

        private void BtnClick_Back(object sender, RoutedEventArgs e)
        {
            _main.Content = new PageIntro(window: _main);
        }
    }
}
