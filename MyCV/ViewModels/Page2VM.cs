using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using MyCV.Pages;
using Services;
using Services.Enums;

namespace MyCV.ViewModels
{
    public class Page2VM
    {
        private readonly int _btnHeight = 55;
        private readonly int _btnWidth = 55;
        private readonly int _sizeX = 8;
        private readonly int _sizeY = 8;
        private readonly Page2 _page;
        

        public MineSweeperService MineService;
        public Page2VM(Page2 page)
        {
            _page = page;
            MineService = new MineSweeperService();
            GeneratePlayingField();
        }

        private void GeneratePlayingField()
        {      

            for (int i = 0; i < _sizeX; i++)
            {
                RowDefinition r = new RowDefinition();
                r.Height = new GridLength(pixels: _btnHeight);
                _page.ButtonsLayoutGrid.RowDefinitions.Add(value: r);
                for (int j = 0; j < _sizeY; j++)
                {
                    ColumnDefinition c = new ColumnDefinition();
                    c.Width = new GridLength(pixels: _btnWidth);
                    _page.ButtonsLayoutGrid.ColumnDefinitions.Add(value: c);

                    Button b = new Button
                    {
                        //Content = i + " " + j,
                        Name = "Button" + (10*i + j + 1),
                        Margin = new Thickness(uniformLength: 2),
                        
                    };
                    b.Background = Brushes.DodgerBlue;
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

        public void GameClickLeft(object sender)
        {
            bool gameHasEnded = MineService.RevealButton(btn: sender as Button);
            if (gameHasEnded)
            {
                GameHasEnded();
            }
        }

        public void GameClickRight(object sender)
        {
            bool gameHasEnded = MineService.MarkButton(btn: sender as Button);
            if (gameHasEnded)
            {
                GameHasEnded();
            }
        }

        private void GameHasEnded()
        {
            switch (MineService.GameEndResult)
            {
                case GameEndResult.Win:
                    _page.ContinueButton.Visibility = Visibility.Visible;
                    break;
                case GameEndResult.Loss:
                    _page.RestartButton.Visibility = Visibility.Visible;
                    break;
                default:
                    break;

            }
        }
    }
}
