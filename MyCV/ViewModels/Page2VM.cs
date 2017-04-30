using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MyCV.Pages;
using Services;

namespace MyCV.ViewModels
{
    public class Page2VM
    {
        private readonly int btnHeight = 50;
        private readonly int btnWidth = 50;
        private readonly int sizeX = 8;
        private readonly int sizeY = 8;

        private Page2 _page;

        public MineSweeperService MineService;
        public Page2VM(Page2 page)
        {
            btnHeight = 50;
            btnWidth = 50;
            _page = page;
            MineService = new MineSweeperService();
            GeneratePlayingField();
        }

        private void GeneratePlayingField()
        {      

            for (int i = 0; i < sizeX; i++)
            {
                RowDefinition r = new RowDefinition();
                r.Height = new GridLength(pixels: btnHeight);
                _page.ButtonsLayoutGrid.RowDefinitions.Add(value: r);
                for (int j = 0; j < sizeY; j++)
                {
                    ColumnDefinition c = new ColumnDefinition();
                    c.Width = new GridLength(pixels: btnWidth);
                    _page.ButtonsLayoutGrid.ColumnDefinitions.Add(value: c);

                    Button b = new Button
                    {
                        //Content = i + " " + j,
                        Name = "Button" + (10*i + j + 1),
                        Margin = new Thickness(uniformLength: 2),
                        
                    };
                    b.Click += new RoutedEventHandler(_page.BtnClick_Left);
                    b.MouseRightButtonDown += new MouseButtonEventHandler(_page.BtnClick_Right);

                    Grid.SetRow(element: b, value: i);
                    Grid.SetColumn(element: b, value: j);

                    _page.ButtonsLayoutGrid.Children.Add(element: b);
                    MineService.AddPlate(btnButton: b, x: i, y: j);
                }
            }
            MineService.SetupGameField();
        }


    }
}
