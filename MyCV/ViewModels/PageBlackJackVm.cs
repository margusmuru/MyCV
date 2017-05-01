using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Models;
using MyCV.Pages;
using Services;

namespace MyCV.ViewModels
{
    public class PageBlackJackVm
    {
        private readonly PageBlackJack _pageBlackJack;

        public BlackJackService BlackJackService;

        public PageBlackJackVm(PageBlackJack pageBlackJack)
        {
            _pageBlackJack = pageBlackJack;
            BlackJackService = new BlackJackService();
            AddDealerCard();
            AddPlayerCard();
        }

        public void AddDealerCard()
        {
            Card card = BlackJackService.GetNextRandomCard();
            Image image = CreateCardForGui(card: card);

            _pageBlackJack.DealerDeck.Children.Add(element: image);
        }

        public void AddPlayerCard()
        {
            Card card = BlackJackService.GetNextRandomCard();
            Image image = CreateCardForGui(card: card);

            _pageBlackJack.PlayerDeck.Children.Add(element: image);
        }

        private Image CreateCardForGui(Card card)
        {
            Image image = new Image();
            image.Width = 70;
            image.Height = 120;
            image.Source = card.BitmapImage;
            return image;
        }
    }
}
