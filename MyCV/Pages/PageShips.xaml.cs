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

namespace MyCV.Pages
{
    /// <summary>
    /// Interaction logic for PageShips.xaml
    /// </summary>
    public partial class PageShips : Page
    {
        private readonly MainWindow _main;

        public PageShips(MainWindow window)
        {
            InitializeComponent();
            this._main = window;
        }

        private void BtnClick_Back(object sender, RoutedEventArgs e)
        {
            _main.Content = new PageIntro(window: _main);
        }

        public void BtnClick_Restart(object sender, RoutedEventArgs e)
        {
            _main.Content = new PageShips(window: _main);
        }

        public void BtnClick_Continue(object sender, RoutedEventArgs e)
        {
            _main.Content = new PageSkills(window: _main);
        }
    }
}
