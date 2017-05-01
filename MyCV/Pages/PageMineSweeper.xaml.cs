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
    /// Interaction logic for PageMineSweeper.xaml
    /// </summary>
    public partial class PageMineSweeper : Page
    {
        private readonly MainWindow _main;
        private readonly PageMineSweeperVm _pageMineSweeperVm;

        public PageMineSweeper(MainWindow window)
        {
            InitializeComponent();
            this._main = window;
            _pageMineSweeperVm = new PageMineSweeperVm(pageMineSweeper: this);
            this.DataContext = _pageMineSweeperVm;
#if DEBUG
            Trace.WriteLine(message: "PageMineSweeper xaml.cs loaded");
#endif
        }

        private void BtnClick_Back(object sender, RoutedEventArgs e)
        {
            _main.Content = new PageIntro(window: _main);
        }

        public void BtnClick_Left(object sender, RoutedEventArgs e)
        {
#if DEBUG
            Trace.WriteLine(message: "Btn LeftClick");
#endif
            _pageMineSweeperVm.GameClickLeft(sender: sender);
        }

        public void BtnClick_Right(object sender, RoutedEventArgs e)
        {
#if DEBUG
            Trace.WriteLine(message: "Btn RightClick");
#endif
            _pageMineSweeperVm.GameClickRight(sender: sender);
        }

        public void BtnClick_Restart(object sender, RoutedEventArgs e)
        {
#if DEBUG
            Trace.WriteLine(message: "Btn Restart MineSweeper");
#endif
            _main.Content = new PageMineSweeper(window: _main);
        }

        public void BtnClick_Continue(object sender, RoutedEventArgs e)
        {
#if DEBUG
            Trace.WriteLine(message: "Btn Continue from MineSweeper");
#endif
            _main.Content = new PageEducation(window: _main);
        }
    }
}
