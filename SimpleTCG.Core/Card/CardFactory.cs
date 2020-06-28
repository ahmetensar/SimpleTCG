using System;
using System.Linq;

namespace SimpleTCG.Core.Card
{
    public static class CardFactory
    {
        private static readonly Random _random = new Random();

        public static DamageCard[] GenerateDamageCards(int lowerDamageProximity, int upperDamageProximity, int[] manaCosts)
        {
            return manaCosts
                .Select(manaCost => 
                {
                    int lowerBound = manaCost >= lowerDamageProximity 
                        ? manaCost - lowerDamageProximity
                        : 0;
                    int upperBound = manaCost + upperDamageProximity + 1;
                    int damage = manaCost > 0 
                        ? _random.Next(lowerBound, upperBound) 
                        : 0;
                    return new DamageCard(manaCost, damage);
                })
                .ToArray();
        }
    }
}
