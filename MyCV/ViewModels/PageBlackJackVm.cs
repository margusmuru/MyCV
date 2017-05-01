using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Models;
using MyCV.Annotations;
using MyCV.Pages;
using Services;

namespace MyCV.ViewModels
{
    public class PageBlackJackVm : INotifyPropertyChanged
    {
        private readonly PageBlackJack _pageBlackJack;

        public BlackJackService BlackJackService;

        private int _dealerValue;
        private bool _dealerContinues;
        private bool _playerContinues;
        public int DealerValue                          
        {
            get { return _dealerValue; }
            set { _dealerValue = value; OnPropertyChanged(propertyName: nameof(DealerValue)); }
        }

        private int _playerValue;
        public int PlayerValue
        {
            get { return _playerValue; }
            set { _playerValue = value; OnPropertyChanged(propertyName: nameof(PlayerValue)); }
        }

        private int _winCount;
        public int WinCount
        {
            get { return _winCount; }
            set { _winCount = value; OnPropertyChanged(propertyName: nameof(WinCount)); }
        }


        public PageBlackJackVm(PageBlackJack pageBlackJack)
        {
            _pageBlackJack = pageBlackJack;
            BlackJackService = new BlackJackService();

            StartNewRound();
        }

        public void AddDealerCard()
        {
            Card card = BlackJackService.GetNextRandomCard();
            Image image = CreateCardForGui(card: card);
            DealerValue = _dealerValue + card.CardValue;
            _pageBlackJack.DealerDeck.Children.Add(element: image);
        }

        public void AddPlayerCard()
        {
            Card card = BlackJackService.GetNextRandomCard();
            Image image = CreateCardForGui(card: card);
            PlayerValue = _playerValue + card.CardValue;
            _pageBlackJack.PlayerDeck.Children.Add(element: image);
        }

        private Image CreateCardForGui(Card card)
        {
            Image image = new Image();
            image.Width = 90;
            image.Height = 150;
            image.Margin = new Thickness(uniformLength: 5);
            image.Source = card.BitmapImage;
            return image;
        }

        public void Deal()
        {
            AddPlayerCard();
            if (_playerValue >= 21)
            {
                _playerContinues = false;
                _dealerContinues = false;
                _pageBlackJack.DealButton.IsEnabled = false;
                _pageBlackJack.PassButton.IsEnabled = false;
            }
            else
            {
                if (_dealerValue < 17)
                {
                    AddDealerCard();
                    if (_dealerValue >= 21)
                    {
                        _dealerContinues = false;
                        _playerContinues = false;
                    }
                }
                else
                {
                    _dealerContinues = false;
                }
            }
            DecideWinner();
        }

        public void Pass()
        {
            _playerContinues = false;
            _pageBlackJack.DealButton.IsEnabled = false;
            _pageBlackJack.PassButton.IsEnabled = false;
            while (_dealerValue < 17)
            {
                AddDealerCard();
            }
            if (_dealerValue >= 21)
            {
                _playerContinues = false;
            }
            _dealerContinues = false;
            DecideWinner();
        }

        private void DecideWinner()
        {
            if (!_dealerContinues && !_playerContinues)
            {
                if (_dealerValue >= 21 || (_playerValue > _dealerValue && _playerValue <= 21))
                {
                    if (_dealerValue == 21)
                    {
                        //LOST
                        _pageBlackJack.ResultMessage.Text = "You Lost!";
                    }
                    else
                    {
                        //WIN
                        _winCount++;
                        _pageBlackJack.ResultMessage.Text = "You Win!";
                        _pageBlackJack.WinCount.Text = "Wins: " + _winCount + "/5";
                    }
                    
                    if (WinCount == 5)
                    {
                        _pageBlackJack.RestartButton.Visibility = Visibility.Collapsed;
                        _pageBlackJack.ContinueButton.Visibility = Visibility.Visible;
                    }
                }else if (_playerValue == _dealerValue)
                {
                    _pageBlackJack.ResultMessage.Text = "Tie!";
                }
                else
                {
                    //LOST
                    _pageBlackJack.ResultMessage.Text = "You Lost!";
                }
            }
        }

        public void PlayAgain()
        {
            StartNewRound();
        }

        private void StartNewRound()
        {
            _pageBlackJack.DealerDeck.Children
                .RemoveRange(index: 0, count: _pageBlackJack.DealerDeck.Children.Count);
            _pageBlackJack.PlayerDeck.Children
                .RemoveRange(index: 0, count: _pageBlackJack.PlayerDeck.Children.Count);
            _playerValue = 0;
            _dealerValue = 0;
            _dealerContinues = true;
            _playerContinues = true;
            _pageBlackJack.DealButton.IsEnabled = true;
            _pageBlackJack.PassButton.IsEnabled = true;
            _pageBlackJack.ResultMessage.Text = "";

            AddDealerCard();
            AddPlayerCard();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
