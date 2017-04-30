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
    /// Interaction logic for Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {
        private readonly MainWindow _main;

        private readonly Page2VM _page2Vm;
        public Page2(MainWindow window)
        {
            InitializeComponent();
            this._main = window;
            _page2Vm = new Page2VM(page: this);
            this.DataContext = _page2Vm;
#if DEBUG
            Trace.WriteLine(message: "Page2 xaml.cs loaded");
#endif
        }

        private void BtnClick_Back(object sender, RoutedEventArgs e)
        {
            _main.Content = new Page1(window: _main);
        }

        public void BtnClick_Left(object sender, RoutedEventArgs e)
        {
#if DEBUG
            Trace.WriteLine(message: "Btn LeftClick");
#endif
            _page2Vm.GameClickLeft(sender: sender);
        }

        public void BtnClick_Right(object sender, RoutedEventArgs e)
        {
#if DEBUG
            Trace.WriteLine(message: "Btn RightClick");
#endif
            _page2Vm.GameClickRight(sender: sender);
        }

        public void BtnClick_Restart(object sender, RoutedEventArgs e)
        {
#if DEBUG
            Trace.WriteLine(message: "Btn Restart MineSweeper");
#endif
            _main.Content = new Page2(window: _main);
        }
    }
}
