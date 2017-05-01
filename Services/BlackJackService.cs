using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Models.Enums;

namespace Services
{
    public class BlackJackService
    {
        private List<Card> Cards { get; }
        private Random _rnd;

        public BlackJackService()
        {
            Cards = new List<Card>();
            _rnd = new Random();
            GenerateCardDeck();
        }

        private void GenerateCardDeck()
        {
            for (int i = 2; i < 15; i++)
            {
                Cards.Add(item: new Card(cardValue: i, cardType: CardType.Clubs));
                Cards.Add(item: new Card(cardValue: i, cardType: CardType.Diamonds));
                Cards.Add(item: new Card(cardValue: i, cardType: CardType.Spades));
                Cards.Add(item: new Card(cardValue: i, cardType: CardType.Hearts));
            }
        }

        public Card GetNextRandomCard()
        {
             
            return Cards[index: _rnd.Next(maxValue: Cards.Count)];
        }
    }
}
