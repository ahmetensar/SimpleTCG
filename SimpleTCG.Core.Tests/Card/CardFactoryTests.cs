using SimpleTCG.Core.Card;
using System.Linq;
using Xunit;

namespace SimpleTCG.Core.Tests.Card
{
    public class CardFactoryTests
    {
        [Theory]
        [InlineData(0, 0, 1, 1, 2, 2, 3, 3, 4, 5, 6, 7)]
        public void Should_Generate_Damage_Cards_With_Given_ManaCosts(params int[] manaCosts)
        {
            DamageCard[] cards = CardFactory.GenerateDamageCards(0, 0, manaCosts);
            var cardManaCosts = cards.Select(c => c.ManaCost).ToArray();

            Assert.Equal(manaCosts, cardManaCosts);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(10, 50)]
        public void Should_Generate_Zero_Damage_Cards_With_Zero_Mana_Cost(int lowerProximity, int upperProximity)
        {
            var zeroArray = new int[10];

            DamageCard[] cards = CardFactory.GenerateDamageCards(lowerProximity, upperProximity, zeroArray);
            var cardDamages = cards.Select(c => c.Damage).ToArray();

            Assert.Equal(zeroArray, cardDamages);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(3, 5)]
        [InlineData(10, 50)]
        public void Should_Generate_Damage_Cards_Within_Bounds(int lowerProximity, int upperProximity)
        {
            var manaCosts = new int[] { 1, 1, 2, 3, 4, 5, 6, 7, 7, 8 };

            DamageCard[] cards = CardFactory.GenerateDamageCards(lowerProximity, upperProximity, manaCosts);
            var cardDamages = cards.Select(c => c.Damage).ToArray();

            for (int i = 0; i < manaCosts.Length; i++)
            {
                int damage = cards[i].Damage;
                int lowerBound = manaCosts[i] - lowerProximity;
                int upperBound = manaCosts[i] + upperProximity;
                Assert.InRange(damage, lowerBound, upperBound);
            }
        }
    }
}
