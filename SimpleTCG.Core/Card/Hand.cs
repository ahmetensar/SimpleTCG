using System;
using System.Collections.Generic;

namespace SimpleTCG.Core.Card
{
    public class Hand : List<ICard>, IHand
    {
        public IList<ICard> Deck { get; }
        public int InitialSize { get; }
        public int MaxSize { get; }
        public bool CanDraw => Deck.Count > 0;

        private readonly Random _random;

        public Hand(IEnumerable<ICard> deckCards, int initialSize, int maxSize)
        {
            Deck = new List<ICard>(deckCards);
            InitialSize = initialSize;
            MaxSize = maxSize;

            _random = new Random();

            for (int i = 0; i < initialSize; i++)
            {
                Draw();
            }
        }

        public void Draw()
        {
            if (CanDraw)
            {
                int index = _random.Next(Deck.Count);
                // overload check
                if (Count < MaxSize)
                {
                    Add(Deck[index]);
                }
                Deck.RemoveAt(index);
            }
        }
    }
}
