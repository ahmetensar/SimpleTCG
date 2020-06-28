using Moq;
using SimpleTCG.Core.Card;
using SimpleTCG.Core.Player;
using Xunit;

namespace SimpleTCG.Core.Tests.Card
{
    public class DamageCardTests
    {
        private readonly IPlayer _playerOne;
        private readonly IPlayer _playerTwo;

        public DamageCardTests()
        {
            var mockHand = new Mock<IHand>();
            int initialHealth = 30;
            int initialMana = 10;
            int maxMana = 10;

            _playerOne = new SimplePlayer("PlayerOne", mockHand.Object, initialHealth, initialMana, maxMana);
            _playerTwo = new SimplePlayer("PlayerTwo", mockHand.Object, initialHealth, initialMana, maxMana);
        }

        [Theory]
        [InlineData(1,1)]
        [InlineData(3, 5)]
        [InlineData(10, 10)]
        public void Use_Should_Damage_Opponent(int manaCost, int damage)
        {
            var sut = new DamageCard(manaCost, damage);
            int initialHealth = _playerTwo.Health;

            sut.Use(_playerOne, _playerTwo);

            int receivedDamage = initialHealth - _playerTwo.Health;
            Assert.Equal(damage, receivedDamage);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(3, 5)]
        [InlineData(10, 10)]
        public void Use_Should_Use_Mana(int manaCost, int damage)
        {
            var sut = new DamageCard(manaCost, damage);
            int initialMana = _playerOne.Mana;

            sut.Use(_playerOne, _playerTwo);

            int usedMana = initialMana - _playerOne.Mana;
            Assert.Equal(manaCost, usedMana);
        }
    }
}
