using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using MyCV.Annotations;
using MyCV.Pages;
using Services;
using Services.Enums;

namespace MyCV.ViewModels
{
    public class PageMineSweeperVm : INotifyPropertyChanged
    {
        private readonly int _btnHeight = 55;
        private readonly int _btnWidth = 55;
        private readonly int _sizeX = 8;
        private readonly int _sizeY = 8;
        private readonly PageMineSweeper _pageMineSweeper;

        private int _minesLeft;

        public int MinesLeft
        {
            get { return _minesLeft; }
            set { _minesLeft = value; OnPropertyChanged(propertyName: nameof(MinesLeft));}
        }

        public MineSweeperService MineService;

        public PageMineSweeperVm(PageMineSweeper pageMineSweeper)
        {
            _pageMineSweeper = pageMineSweeper;
            MineService = new MineSweeperService();
            GeneratePlayingField();
        }

        private void GeneratePlayingField()
        {      

            for (int i = 0; i < _sizeX; i++)
            {
                RowDefinition r = new RowDefinition();
                r.Height = new GridLength(pixels: _btnHeight);
                _pageMineSweeper.ButtonsLayoutGrid.RowDefinitions.Add(value: r);
                for (int j = 0; j < _sizeY; j++)
                {
                    ColumnDefinition c = new ColumnDefinition();
                    c.Width = new GridLength(pixels: _btnWidth);
                    _pageMineSweeper.ButtonsLayoutGrid.ColumnDefinitions.Add(value: c);

                    Button b = new Button
                    {
                        //Content = i + " " + j,
                        Name = "Button" + (10*i + j + 1),
                        Margin = new Thickness(uniformLength: 2),
                        
                    };
                    b.Background = Brushes.DodgerBlue;
                    b.Click += new RoutedEventHandler(_pageMineSweeper.BtnClick_Left);
                    b.MouseRightButtonDown += new MouseButtonEventHandler(_pageMineSweeper.BtnClick_Right);

                    Grid.SetRow(element: b, value: i);
                    Grid.SetColumn(element: b, value: j);

                    _pageMineSweeper.ButtonsLayoutGrid.Children.Add(element: b);
                    MineService.AddPlate(btnButton: b, x: i, y: j);
                }
            }
            MineService.SetupGameField();
            MinesLeft = MineService.MinesLeft;
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
            MinesLeft = MineService.MinesLeft;
            if (gameHasEnded)
            {
                GameHasEnded();
            }
        }

        private void GameHasEnded()
        {
            MinesLeft = 0;
            switch (MineService.GameEndResult)
            {
                case GameEndResult.Win:
                    _pageMineSweeper.RestartButton.Visibility = Visibility.Collapsed;
                    _pageMineSweeper.ContinueButton.Visibility = Visibility.Visible;
                    _pageMineSweeper.ResultMessage.Text = "You Win!";
                    break;
                case GameEndResult.Loss:
                    _pageMineSweeper.ResultMessage.Text = "You Lose!";
                    break;
                default:
                    break;

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
