using Moq;
using SimpleTCG.Core.Card;
using SimpleTCG.Core.Player;
using SimpleTCG.Core.State;
using System.Linq;
using Xunit;

namespace SimpleTCG.Core.Tests.State
{
    public class GameStateTests
    {
        private readonly Mock<IPlayer> _mockPlayerOne;
        private readonly Mock<IPlayer> _mockPlayerTwo;

        public GameStateTests()
        {
            _mockPlayerOne = new Mock<IPlayer>();
            _mockPlayerTwo = new Mock<IPlayer>();
        }

        [Fact]
        public void Should_Construct_Properly()
        {
            _mockPlayerOne.Setup(p => p.Health).Returns(30);
            _mockPlayerTwo.Setup(p => p.Health).Returns(30);

            var sut = new GameState(_mockPlayerOne.Object, _mockPlayerTwo.Object);

            Assert.True(_mockPlayerOne.Object.Equals(sut.ActivePlayer));
            Assert.True(_mockPlayerTwo.Object.Equals(sut.OpponentPlayer));
            Assert.Null(sut.Winner);
            Assert.False(sut.IsFinished);
            Assert.Equal(EActivePlayerState.PlayerOne, sut.ActivePlayerState);
        }

        [Fact]
        public void Use_Should_Remove_Card()
        {
            var mockCard = new Mock<ICard>();
            var hand = new Hand(new[] { mockCard.Object }, 1, 1); //TODO mock Hand
            _mockPlayerOne.Setup(p => p.Hand).Returns(hand);

            var sut = new GameState(_mockPlayerOne.Object, _mockPlayerTwo.Object);

            sut.Use(mockCard.Object);

            Assert.Empty(hand);
        }

        [Fact]
        public void Use_Should_Use_Card()
        {
            var mockCard = new Mock<ICard>();
            mockCard.Setup(c => c.Use(It.IsAny<IPlayer>(), It.IsAny<IPlayer>()));
            var hand = new Hand(new[] { mockCard.Object }, 1, 1);
            _mockPlayerOne.Setup(p => p.Hand).Returns(hand);

            var sut = new GameState(_mockPlayerOne.Object, _mockPlayerTwo.Object);

            sut.Use(mockCard.Object);

            mockCard.Verify(c => c.Use(It.IsAny<IPlayer>(), It.IsAny<IPlayer>()), Times.Once);
        }

        [Fact]
        public void Next_Should_Switch_Players()
        {
            var mockCard = new Mock<ICard>();
            mockCard.Setup(c => c.Use(It.IsAny<IPlayer>(), It.IsAny<IPlayer>()));
            var hand = new Hand(new[] { mockCard.Object }, 0, 1);
            _mockPlayerOne.Setup(p => p.Health).Returns(30);
            _mockPlayerOne.Setup(p => p.Hand).Returns(hand);
            _mockPlayerTwo.Setup(p => p.Health).Returns(30);
            _mockPlayerTwo.Setup(p => p.Hand).Returns(hand);

            var sut = new GameState(_mockPlayerOne.Object, _mockPlayerTwo.Object);

            sut.Next();

            Assert.True(_mockPlayerTwo.Object.Equals(sut.ActivePlayer));
            Assert.True(_mockPlayerOne.Object.Equals(sut.OpponentPlayer));
        }

        [Fact]
        public void Next_Should_Bleed_Players_With_Empty_Deck()
        {
            var mockCard = new Mock<ICard>();
            mockCard.Setup(c => c.Use(It.IsAny<IPlayer>(), It.IsAny<IPlayer>()));
            var handOne = new Hand(new[] { mockCard.Object }, 0, 1);
            var handTwo = new Hand(Enumerable.Empty<ICard>(), 0, 0);
            _mockPlayerOne.Setup(p => p.Health).Returns(30);
            _mockPlayerOne.Setup(p => p.Hand).Returns(handOne);
            _mockPlayerTwo.Setup(p => p.Health).Returns(30);
            _mockPlayerTwo.Setup(p => p.Hand).Returns(handTwo);
            _mockPlayerTwo.Setup(p => p.ReceiveDamage(1));

            var sut = new GameState(_mockPlayerOne.Object, _mockPlayerTwo.Object);

            sut.Next();

            Assert.True(_mockPlayerOne.Object.Equals(sut.ActivePlayer));
            Assert.True(_mockPlayerTwo.Object.Equals(sut.OpponentPlayer));
            _mockPlayerTwo.Verify(p => p.ReceiveDamage(1), Times.Once);
        }

        [Fact]
        public void Should_Finish_If_A_Players_Health_Is_Zero()
        {
            _mockPlayerTwo.Setup(p => p.Health).Returns(30);

            var sut = new GameState(_mockPlayerOne.Object, _mockPlayerTwo.Object);

            Assert.True(_mockPlayerTwo.Object.Equals(sut.Winner));
            Assert.True(sut.IsFinished);
        }
    }
}
