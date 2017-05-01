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
    /// Interaction logic for PageSkills.xaml
    /// </summary>
    public partial class PageSkills : Page
    {
        private readonly MainWindow _main;
        public PageSkills()
        {
            InitializeComponent();
        }

        private void BtnClick_Back(object sender, RoutedEventArgs e)
        {
            _main.Content = new PageIntro(window: _main);
        }
    }
}
