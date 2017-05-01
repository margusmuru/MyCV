using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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

namespace MyCV.ViewModels
{
    public class PageShipsVm : INotifyPropertyChanged
    {
        private readonly int _sizeX = 7;
        private readonly int _sizeY = 7;
        private readonly int _btnHeight;
        private readonly int _btnWidth;
        private readonly PageShips _pageShips;

        private readonly ShipsService _enemyShipsService;
        private readonly ShipsService _playerShipsService;

        private int _enemyShipsLeft;
        public int EnemyShipsLeft
        {
            get { return _enemyShipsLeft; }
            set { _enemyShipsLeft = value; OnPropertyChanged(propertyName: nameof(EnemyShipsLeft)); }
        }

        private int _playerShipsLeft;
        public int PlayerShipsLeft
        {
            get { return _playerShipsLeft; }
            set { _playerShipsLeft = value; OnPropertyChanged(propertyName: nameof(PlayerShipsLeft)); }
        }

        public PageShipsVm(PageShips pageShips)
        {
            _pageShips = pageShips;
            _btnHeight = 200 / _sizeY;
            _btnWidth = 200 / _sizeX;
            _enemyShipsService = new ShipsService();
            _playerShipsService = new ShipsService();
            GeneratePlayingField(forPlayer: true);
            GeneratePlayingField(forPlayer: false);
            _enemyShipsService.GenerateShips(count: 10);
            _playerShipsService.GenerateShips(count: 10);
            EnemyShipsLeft = _enemyShipsService.ShipsLeft;
            PlayerShipsLeft = _playerShipsService.ShipsLeft;
        }

        private void GeneratePlayingField(bool forPlayer)
        {

            for (int i = 0; i < _sizeX; i++)
            {
                RowDefinition r = new RowDefinition();
                r.Height = new GridLength(pixels: _btnHeight);

                if(forPlayer)
                {
                    _pageShips.PlayerField.RowDefinitions.Add(value: r);
                }
                else
                {
                    _pageShips.EnemyField.RowDefinitions.Add(value: r);
                }

                for (int j = 0; j < _sizeY; j++)
                {
                    ColumnDefinition c = new ColumnDefinition();
                    c.Width = new GridLength(pixels: _btnWidth);
                    if(forPlayer)
                    {
                        _pageShips.PlayerField.ColumnDefinitions.Add(value: c);
                    }
                    else
                    {
                        _pageShips.EnemyField.ColumnDefinitions.Add(value: c);
                    }

                    Button b = new Button
                    {
                        //Content = i + " " + j,
                        Name = "Button" + (10 * i + j + 1),
                        Margin = new Thickness(uniformLength: 2),

                    };
                    b.Background = Brushes.DodgerBlue;
                    if(!forPlayer)
                    {
                        b.Click += new RoutedEventHandler(_pageShips.BtnClick_Left);
                    }

                    Grid.SetRow(element: b, value: i);
                    Grid.SetColumn(element: b, value: j);
                    if(forPlayer)
                    {
                        _pageShips.PlayerField.Children.Add(element: b);
                        _playerShipsService.AddPlate(btnButton: b, x: i, y: j);
                    }
                    else
                    {
                        _pageShips.EnemyField.Children.Add(element: b);
                        _enemyShipsService.AddPlate(btnButton: b, x: i, y: j);
                    }
                }
            }

        }

        public void GameLeftClick(Button btn)
        {
            bool resultEnemy = _enemyShipsService.RevealButton(btn: btn);
#if DEBUG
            Trace.WriteLine(message: "resultEnemy " + resultEnemy);
#endif
            bool resultPlayer = _playerShipsService.RevealRandomButton();
#if DEBUG
            Trace.WriteLine(message: "resultPlayer " + resultPlayer);
#endif
            //Check if game should end
            PlayerShipsLeft = _playerShipsService.ShipsLeft;
            EnemyShipsLeft = _enemyShipsService.ShipsLeft;

            if (_playerShipsLeft == 0 || _enemyShipsLeft == 0)
            {
                EndGame();
            }
        }

        private void EndGame()
        {
            _playerShipsService.RevealAll();
            _enemyShipsService.RevealAll();
            if (_playerShipsLeft > _enemyShipsLeft)
            {
                //WIN
                _pageShips.ResultMessage.Text = "You won!";
                _pageShips.RestartButton.Visibility = Visibility.Collapsed;
                _pageShips.ContinueButton.Visibility = Visibility.Visible;
            }
            else if (_playerShipsLeft < _enemyShipsLeft)
            {
                //LOSS
                _pageShips.ResultMessage.Text = "You lost!";
            }
            else
            {
                //TIE
                _pageShips.ResultMessage.Text = "Tied!";
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
