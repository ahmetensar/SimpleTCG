using Moq;
using SimpleTCG.Core.Card;
using SimpleTCG.Core.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SimpleTCG.Core.Tests.Player
{
    public class SimplePlayerTests
    {
        private readonly Mock<IHand> _mockHand;

        public SimplePlayerTests()
        {
            _mockHand = new Mock<IHand>();
        }

        [Theory]
        [InlineData(30, 1, 10)]
        public void Should_Construct_Properly(int initialHealth, int initialMana, int maxMana)
        {
            var sut = new SimplePlayer("Player", _mockHand.Object, initialHealth, initialMana, maxMana);

            Assert.True(_mockHand.Object.Equals(sut.Hand));
            Assert.Equal(initialHealth, sut.Health);
            Assert.Equal(initialMana, sut.Mana);
            Assert.Equal(initialMana, sut.ManaSlot);
        }

        [Fact]
        public void Should_Not_Be_Able_To_Play_If_Hand_Is_Empty()
        {
            _mockHand.Setup(c => c.GetEnumerator()).Returns(Enumerable.Empty<ICard>().GetEnumerator());
            var sut = new SimplePlayer("Player", _mockHand.Object, 30, 0, 0);

            Assert.False(sut.CanPlay);
        }

        [Fact]
        public void Should_Not_Be_Able_To_Play_If_Mana_Is_Not_Enough()
        {
            var mockCard = new Mock<ICard>();
            mockCard.Setup(c => c.ManaCost).Returns(2);
            var list = new List<ICard> { mockCard.Object };
            _mockHand.Setup(c => c.GetEnumerator()).Returns(list.GetEnumerator());
            var sut = new SimplePlayer("Player", _mockHand.Object, 30, 1, 1);

            Assert.False(sut.CanPlay);
        }

        [Fact]
        public void Should_Be_Able_To_Play_If_Mana_Is_Enough()
        {
            var mockCard = new Mock<ICard>();
            mockCard.Setup(c => c.ManaCost).Returns(1);
            var list = new List<ICard> { mockCard.Object };
            _mockHand.Setup(c => c.GetEnumerator()).Returns(list.GetEnumerator());         
            var sut = new SimplePlayer("Player", _mockHand.Object, 30, 1, 1);

            Assert.True(sut.CanPlay);
        }

        [Theory]
        [InlineData(3,5)]
        [InlineData(5,3)]
        public void ShouldUseMana(int usedMana, int initialMana)
        {
            var sut = new SimplePlayer("Player", _mockHand.Object, 30, initialMana, initialMana);

            sut.UseMana(usedMana);

            int remainingMana = Math.Max(initialMana - usedMana, 0);
            Assert.Equal(remainingMana, sut.Mana);
        }


        [Theory]
        [InlineData(10, 5)]
        [InlineData(10, 15)]
        public void ShouldReceiveDamage(int damage, int initialHealth)
        {
            var sut = new SimplePlayer("Player", _mockHand.Object, initialHealth, 0, 0);

            sut.ReceiveDamage(damage);

            int remainingHealth = Math.Max(initialHealth - damage, 0);
            Assert.Equal(remainingHealth, sut.Health);
        }

        [Theory]
        [InlineData(0, 10)]
        [InlineData(5, 10)]
        [InlineData(15, 10)]
        public void Should_Refresh_Mana(int initialMana, int maxMana)
        {
            var sut = new SimplePlayer("Player", _mockHand.Object, 30, initialMana, maxMana);

            sut.UseMana(initialMana);
            sut.RefreshMana();

            if (initialMana < maxMana)
            {
                Assert.Equal(initialMana + 1, sut.Mana);
                Assert.Equal(initialMana + 1, sut.ManaSlot);
            }
            else
            {
                Assert.Equal(initialMana, sut.Mana);
                Assert.Equal(initialMana, sut.ManaSlot);
            }

        }
    }
}
