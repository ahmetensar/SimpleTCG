using SimpleTCG.Core.Card;
using SimpleTCG.Core.State;
using System;
using System.Linq;

namespace SimpleTCG.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var random = new Random();

            Console.WriteLine("Sample run");
            Console.WriteLine();

            var state = GameStateFactory.GenerateDefaultState();
            while (!state.IsFinished)
            {
                Console.WriteLine($"ActivePlayer: {state.ActivePlayer.Name}, Health: {state.ActivePlayer.Health}, Mana: {state.ActivePlayer.Mana}");
                Console.WriteLine($"OpponentPlayer: {state.OpponentPlayer.Name}, Health: {state.OpponentPlayer.Health}, Mana: {state.OpponentPlayer.Mana}");

                Console.WriteLine("Draw, use and next");
                var hand = state.ActivePlayer.Hand;
                hand.Draw();
                Console.WriteLine($"Remaining deck count: {hand.Deck.Count}");
                Console.WriteLine(@$"Hand: {string.Join(",", hand.Select(c =>
                {
                    if (c is DamageCard damageCard)
                    {
                        return $"[{damageCard.ManaCost},{damageCard.Damage}]";
                    }
                    return $"[{c.ManaCost}]";
                }))}");

                if (hand.Any())
                {
                    int index = random.Next(hand.Count);
                    Console.WriteLine($"Using card: {index}");
                    var card = hand[index];
                    state.Use(card);
                    state.Next();
                }
                Console.WriteLine();
            }
            Console.WriteLine($"Winner: {state.Winner.Name}");
        }
    }
}
