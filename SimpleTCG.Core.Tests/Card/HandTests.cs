using Moq;
using SimpleTCG.Core.Card;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SimpleTCG.Core.Tests.Card
{
    public class HandTests
    {
        private readonly IList<ICard> _deckCards;

        public HandTests()
        {
            _deckCards = new List<ICard>();
            var mockCard = new Mock<ICard>();

            for (int i = 0; i < 20; i++)
            {
                _deckCards.Add(mockCard.Object);
            }
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(3, 5)]
        public void Should_Construct_Properly(int initialSize, int maxSize)
        {
            var sut = new Hand(_deckCards, initialSize, maxSize);

            Assert.Equal(initialSize, sut.InitialSize);
            Assert.Equal(maxSize, sut.MaxSize);
            Assert.Equal(_deckCards.Count - initialSize, sut.Deck.Count);
            Assert.Equal(initialSize, sut.Count);
        }

        [Fact]
        public void Should_Not_Be_Able_To_Draw_With_Empty_Deck()
        {
            var sut = new Hand(Enumerable.Empty<ICard>(), 3, 5);

            Assert.False(sut.CanDraw);
        }

        [Fact]
        public void Should_Draw()
        {
            int initialSize = 3;
            int maxSize = 5;
            var sut = new Hand(_deckCards, initialSize, maxSize);

            Assert.True(sut.CanDraw);

            sut.Draw();

            Assert.Equal(_deckCards.Count - initialSize - 1, sut.Deck.Count);
            Assert.Equal(initialSize + 1, sut.Count);
        }

        [Fact]
        public void Should_Overload()
        {
            int initialSize = 5;
            int maxSize = 5;
            var sut = new Hand(_deckCards, initialSize, maxSize);

            sut.Draw();

            Assert.Equal(_deckCards.Count - initialSize - 1, sut.Deck.Count);
            Assert.Equal(initialSize, sut.Count);
        }
    }
}
