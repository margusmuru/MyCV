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
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        private readonly MainWindow _main;

        private readonly Page1VM _page1Vm;

        public Page1(MainWindow window)
        {
            InitializeComponent();
            this._main = window;
            _page1Vm = new Page1VM();
            this.DataContext = _page1Vm;
#if DEBUG
            Trace.WriteLine(message: "Page1 xaml.cs loaded");
#endif

        }
        private void BtnMainInfo(object sender, RoutedEventArgs e)
        {
            _main.Content = new Page2(window: _main);
        }

        
    }
}
