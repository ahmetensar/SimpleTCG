using SimpleTCG.Core.Player;

namespace SimpleTCG.Core.Card
{
    public interface ICard
    {
        int ManaCost { get; }

        bool Use(IPlayer activePlayer, IPlayer opponentPlayer);
    }
}
