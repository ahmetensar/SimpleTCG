using SimpleTCG.Core.Card;
using SimpleTCG.Core.Player;

namespace SimpleTCG.Core.State
{
    public static class GameStateFactory
    {
        public static IGameState GenerateDefaultState()
        {
            var manaCosts = new int[]
            {
                0, 0, 1, 1, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 5, 5, 6, 6, 7, 8
            };
            var playerOneDeckCards = CardFactory.GenerateDamageCards(0, 2, manaCosts);
            var playerOneHand = new Hand(playerOneDeckCards, 3, 5);
            var playerOne = new SimplePlayer("PlayerOne", playerOneHand, 30, 1, 10);

            var playerTwoDeckCards = CardFactory.GenerateDamageCards(0, 2, manaCosts);
            var playerTwoHand = new Hand(playerTwoDeckCards, 3, 5);
            var playerTwo = new SimplePlayer("PlayerTwo", playerTwoHand, 30, 1, 10);

            return new GameState(playerOne, playerTwo);
        }
    }
}
