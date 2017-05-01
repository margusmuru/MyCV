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
    /// Interaction logic for PageBlackJack.xaml
    /// </summary>
    public partial class PageBlackJack : Page
    {
        private readonly MainWindow _main;
        private readonly PageBlackJackVm _pageBlackJackVm;

        public PageBlackJack(MainWindow window)
        {
            InitializeComponent();
            _main = window;
            _pageBlackJackVm = new PageBlackJackVm(pageBlackJack: this);
            this.DataContext = _pageBlackJackVm;
        }

        private void BtnClick_Back(object sender, RoutedEventArgs e)
        {
            _main.Content = new PageIntro(window: _main);
        }

        public void BtnClick_Continue(object sender, RoutedEventArgs e)
        {
            _main.Content = new PageWorkExperience(window: _main);
        }

        public void BtnClick_Restart(object sender, RoutedEventArgs e)
        {
            _main.Content = new PageBlackJack(window: _main);
        }
    }
}
